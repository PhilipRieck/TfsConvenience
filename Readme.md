TfsConvenience
==============

A library to easily run WIQL queries against a TFS server through c#.

Usage
-----

This library requires Autofac.  See the notes section below

You must do two things
1. Call the "Configurator" passing in a Autofac ContainerBuilder (and later use that container for resolution of a Query)
2. Register a IConnectionParameterProvider instance that will provide the connection parameters (CollectionUri, Username, etc) on request.

Now you can simply use the container to resolve an IQuery, st the QueryString, and call Execute one or more times.  

Note that using the ExecuteAsync is optional, as is hooking into QueryProgress events.

Notes
-----

###Why is this autofac specific? Ever heard of the Common Service Locator?
Yes, I have heard of the [Common Service Locator](http://commonservicelocator.codeplex.com/).  This is autofac specifc because it's ripped right from two projects
I'm using it in, and I strongly prefer [Autofac](http://code.google.com/p/autofac/) over anything else out there.   I will perhaps someday make it more portable (feel free)

###Where is the NuGet package?
It's in a private repo at the moment because it is autofac specific, and usage is a bit odd for a general purpose library.  Again, feel free to Be Somebody.


###License Stuff
Only because I have been told ( somewhat harshly ) that "dumping code into github isn't providing a license".  Sorry.  

This work is "AS IS" and without warranty of any kind, licensed under the [Creative Commons CC0 License](http://creativecommons.org/publicdomain/zero/1.0/)

