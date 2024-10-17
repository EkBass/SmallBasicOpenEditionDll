## SmallBasic Open Edition Dll

SmallBasicOpenEditionDll is part of the broader SmallBasic Open Edition project.

The goal of this project is to develop an interpreter/compiler capable of translating code written in [Microsoft SmallBasic](https://smallbasic-publicwebsite.azurewebsites.net/) into C#. Once translated, the C# code is compiled into an executable using a C# compiler.

**SmallBasicOpenEditionDll.dll** replicates the classes and methods of the original `smallbasic.dll` library included with Microsoft SmallBasic. This makes it easier for the interpreter to convert SmallBasic code into C#.

While achieving 100% compatibility is unlikely, I aim to get as close as possible.

Currently, **SmallBasicOpenEditionDll.dll** is fully functional for use in C# programs. However, it does not process SmallBasic code on its own—that will be handled by the interpreter, which is a separate project. Though there have already been successful proof-of-concept tests for the interpreter, it's still in a rather early, chaotic state. My current focus is on completing this library before shifting my attention to the interpreter and the necessary installation program.

When compiled, **SmallBasicOpenEditionDll.dll** includes all of its dependencies, creating a "single file" compilation.

Once I resolve the remaining minor bugs and improve the documentation, I will release the compiled version of the library. Until then, you can download the source code as a Visual Studio project from here.

If you download the source to compile it yourself, please check the paths in the [`.csproj`](https://github.com/EkBass/SmallBasicOpenEditionDll/blob/master/SmallBasicOpenEditionDll.csproj) file to ensure that all necessary files are correctly referenced on your machine.

- Krisu

`string.Join("krisu", ".virtanen", "@gmai", "l.com")`

