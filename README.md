Hi,

This project (SmallBasicOpenEditionDll) is part of a larger whole, where the goal is to recreate [Microsoft SmallBasic](https://smallbasic-publicwebsite.azurewebsites.net/), at least in its main features.

The first phase of the project, which is exactly this, is to create the same classes as in SmallBasic: **Text**, **Math**, **GraphicsWindow**, and so on. The project is mostly complete, although types for variables, some small tests, and so on are still required.

**Note:** The classes **Array**, **Flickr**, and **Dictionary** have not been created. 
 
**Array** is missing because I have not yet decided on the final way the future compiler will handle arrays. I have a couple of different solutions for this, but I haven’t had time to decide yet.  

**Flickr** is missing because I am simply not very familiar with this service, and their API is completely unknown to me.  

**Dictionary** is a class I’m not sure if I want to spend time on, at least not personally.

Unlike the original SmallBasic, Open Edition converts SmallBasic code into the C# programming language and compiles it afterward. This brings significant advantages in terms of memory management, speed, and extensibility.

Variables will function mostly the same way as in the original SmallBasic. This is made possible by C#’s **dynamic**. However, the idea is that variables will require a suffix of "$" at the end of their names. Not only does this make the programs easier to compile, but I also believe it helps distinguish variables and makes coding a bit easier.

Example SmallBasic program:

```smallbasic
' Example Smallbasic program
x$ = 1
y$ = 2
c$ = "Foo"

TextWindow.WriteLine(x$ + y$ + c$)
SayHello()

Goto MyLabel:
TextWindow.WriteLine("I'm never printed.")

MyLabel:
Program.End()

Sub SayHello
	TextWindow.WriteLine("Hello")
EndSub
```

Converted to C#:

```csharp
// Converted as C#
namespace SmallBasicOpenEditionDll
{
    public static class SB_Program
    {
        // Declare dynamic variables (similar to Smallbasic's dynamic typing)
        public static dynamic x = 1;
        public static dynamic y = 2;
        public static dynamic c = "Foo";

        // Entry point of the program
        public static void Main()
        {
            // Equivalent of TextWindow.WriteLine(x$ + y$ + c$)
            TextWindow.WriteLine(x + y + c);

            // Call the method SayHello()
            SayHello();

            // This line will not be executed
            Goto MyLabel:
            TextWindow.WriteLine("I'm never printed.");

            MyLabel:
            Program.End();
        }

        // The equivalent of SmallBasic's subroutine 'SayHello'
        public static void SayHello()
        {
            TextWindow.WriteLine("Hello");
        }
    }
}
```

**Note:** I work with this while I am working with two job's and such, so this will not more forward in fast speed. Help is appreciated here. Let me know :)
