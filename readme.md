## Scal

__Scal__ uses the OSS projects [Caliburn.Micro][1], [StructureMap][2], [MemBus][3] and [DynamicXaml][4] as well as the reactive Extensions to provide a WPF dev experience
that is in its roots based on Caliburn.Micro but gives you a couple more things into your hand:

* One of the greatest DI Containers there are that gives you a lot more injection power than MEF
* An Event Aggregator that allows you to configure many aspects of moving messages from publishers to subscribers
* An API to create XAML Object trees via code and ways to access and use XAML-based resources from code.
* An API to configure your app and create new scenarios with regard to view location, messaging and more.

Check out the [Sample App][5] for usage guideline. 

### How do I get started?
Include the __ScalBootstrapper__ from the __Scal__-assembly into your App.xaml as a ResourceDictionary (much like in Caliburn.Micro) and then define a class that derives from __ScalConfiguration__. In there you will find an API to further configure your application. Here's the example of the [Sample App][6].

### How do I get to the program arguments?
There is a class named __ProgramArguments__ which the underlying container will gladly provide to you. You can enumerate it which will give you all arguments passed into the application during startup.

### Caliburn.Micro got a new release. Why can't I update it?
As long as CM provides an install script, Scal will reference CM manually to avoid confusion with the different approach to setting up the application. While the process is manually you can still expect an update of the contained CM library as soon as a new Nuget release appears.

  [1]: http://caliburnmicro.codeplex.com
  [2]: https://github.com/structuremap/structuremap
  [3]: https://github.com/flq/MemBus
  [4]: https://github.com/flq/XamlTags
  [5]: https://github.com/flq/Scal/tree/master/SampleApp
  [6]: https://github.com/flq/Scal/blob/master/SampleApp/AppConfiguration.cs