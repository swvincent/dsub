**NOTE: This is the net40 branch, which contains a version of dsub that targets .NET Framework 4.0 Client Profile. The most recent version of dsub is available in the [master branch](https://github.com/swvincent/dsub).**

# dsub

dsub is a class that builds upon .NET's built-in SerialPort class. It adds functionality to deal with multi-threading, error-handling, etc.

It is based on what I've learned from Jan Axelson's book [Serial Port Complete Second Edition](http://janaxelson.com/spc.htm) and the sample applications available on her website. If you're interested in learning more about Serial Ports and/or using them in .NET, I highly recommend checking out her book and website.

dsub uses SerialPort.ReadLine(), so it's required that any data being received has a defined "end of transmission" character, such as a carriage return or line feed. If you have a situation where there isn’t a defined end of transmission character, then dsub won’t work.

## Getting Started

To use dsub, just add the [ComPort class](/Dsub/ComPort.cs) to an existing application.

## Example Application

The GUI application that’s included will read data from the selected serial port and display it in a grid. If a field delimiter is specified, it will use that to break up the data into separate columns in the grid. At the bottom of the screen, you can enter text to be sent. There is also a textbox where errors will be displayed. The application implements all the features of dsub so it provides a good example of how to use it.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [Jan Axelson's Serial Port Central](http://janaxelson.com/serport.htm) contains lots of great information.
* [Com Ports - Technical Information](https://www.developerfusion.com/article/22/com-ports-technical-information/) was used as a reference for several serial port settings.
* [Kean](http://www.kean.com.au/) provided valuable feedback on an error message.
