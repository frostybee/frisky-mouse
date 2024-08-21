# FriskyMouse

FriskyMouse is a free, highly customizable, and open-source utility application for Windows 10 and later.

With FriskyMouse, you can draw a spotlight around your mouse cursor and indicate the location of your mouse left or right clicks using a set of different ripple effects, enabling your audience to follow your mouse movements and actions with ease.

## Features

- Highly customizable mouse pointer highlighter.
- Animated mouse Left/right clicks visual indicator.

## Demo

![alt](screenshots/demo.gif)

## Building from Source

FriskyMouse requires .NET 8 to be installed on your local machine.

1. Clone this repository
2. Open the `.sln` file from the `src` folder in Visual Studio 2022
3. Rebuild the solution

### Publishing a Self-Contained Version

You can publish a self-contained app by executing the following command.

```bat
dotnet publish -c SelfContained -r win-x64 --self-contained -p:PublishReadyToRun=true
```

## Documentation

FriskyMouse documentation can be found at https://friskymouse.github.io/docs/

## License

FriskyMouse is free and open source software licensed under [MIT License](https://mit-license.org/). You can use it in private and commercial projects.
Keep in mind that you must include a copy of the license in your project.
