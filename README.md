# wtw-demo

## Overview

wtw-demo is a sample code project for WTW that contains a very simple implementation of an IoC container. The solution contains three projects:

* DemoIoC - The project containing the IoC implementaion.
* DemoIoC.UnitTests - xUnit tests for the DemoIoC project.
* DemoIoC.ConsoleApp - A simple console application that verifies that the container is working as intended.

## Container Usage

To use the container, simply create in instance of Container, register a type along with the desired object life cycle and then resolve the type through the container. Available object life cycles are LifeCycle.Singleton and LifeCycle.Transient. If no life cycle is specified then the registered type will default to LifeCycle.Transient.

```c#
var container = new Container();
container.Register<ILogger, Logger>(LifeCycle.Singleton);

var myLogger = container.Resolve<ILogger>();
```

## Console Application Usage

The included console application demonstrates registering and resolving singleton and transient objects using both objects that take no construction parameters as well as objects that take other registered type as parameters.

Once the objects are created, the application will print each object's hash code to verify that all singleton objects share a single instance and that all transient objects have a unique instance.
