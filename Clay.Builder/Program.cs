using System.Runtime.CompilerServices;
using static Bullseye.Targets;
using static SimpleExec.Command;

string GetFilePath([CallerFilePath] string path = default!) => path;

string workingDirectory = Path.GetDirectoryName(GetFilePath());
string clayh = Path.Combine(workingDirectory, "src/clay.h");
string claycs = Path.Combine(workingDirectory, "../Clay-cs/Interop/ClayInterop.cs");
string zigoutdll = Path.Combine(workingDirectory, "./zig-out/bin/Clay.dll");
string libdll = Path.Combine(workingDirectory, "../Clay-cs/Clay.dll");

Target("Interop", () =>
{
	var interopArgs = string.Join(' ', [
		"-c generate-file-scoped-namespaces",
		$"--file {clayh}",
		"-n Clay_cs",
		"--methodClassName ClayInterop",
		"--libraryPath Clay",
		$"-o {claycs}",
		"--exclude *Wrapper"
	]);
	Run("ClangSharpPInvokeGenerator", interopArgs, workingDirectory);
	
	// ClangSharpPInvokeGenerator is adding a trailing '}' that breaks compilation
	var text = File.ReadAllText(claycs);
	var idx = text.LastIndexOf('}');
	text = text.Substring(0, idx);
	
	// we write wrappers for all these calls so no public access needed
	text = text.Replace("public static unsafe partial class ClayInterop", "internal static unsafe partial class ClayInterop");
	
	File.WriteAllText(claycs, text);
});

Target("Dll", async () =>
{
	var ZigToolsetPath = Environment.GetEnvironmentVariable("ZigToolsetPath");
	var ZigExePath = Environment.GetEnvironmentVariable("ZigExePath");
	var ZigLibPath = Environment.GetEnvironmentVariable("ZigLibPath");
	var ZigDocPath = Environment.GetEnvironmentVariable("ZigDocPath");

	// clean build
	var oudDir = Path.GetDirectoryName(zigoutdll);
	if (Directory.Exists(oudDir))
	{
		Directory.Delete(oudDir, true);
	}
	
	await RunAsync("zig", "build", workingDirectory);
	Directory.CreateDirectory(Path.GetDirectoryName(libdll));
	File.Delete(libdll);
	File.Copy(zigoutdll, libdll, true);
});

Target("default", DependsOn("Dll", "Interop"));

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException);