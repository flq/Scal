## Scal

__Scal__ uses the OSS projects [Caliburn.Micro][1], [StructureMap][2], [MemBus][3] and [DynamicXaml][4] to provide a WPF dev experience
that is similar to Caliburn.Micro but gives you a couple more things into your hand:

* One of the greatest DI Containers there are that gives you a lot more injection power than MEF
* An Event Aggregator that allows you to configure many aspects of moving messages from publishers to subscribers
* An API to create XAML Object trees via code and ways to access and use XAML-based resources from code.

Check out the [Sample App][5] for usage guideline. 

## Getting Started
You should include the __ScalBootstrapper__ from the __Scal__-assembly into your App.xaml and then define a class that derives from __ScalConfiguration__. In there you will find an API to further configure your application.

  [1]: http://caliburnmicro.codeplex.com
  [2]: https://github.com/structuremap/structuremap
  [3]: https://github.com/flq/MemBus
  [4]: https://github.com/flq/XamlTags
  [5]: https://github.com/flq/Scal/tree/master/SampleApp