TfsConvenience
==============

A library to easily run WIQL queries against a TFS server through c#.

Usage
-----

Create a new Query, passing a ConnectionParameters instance.

If you wish to, use Connection to test a ConnectionParameters instance;

You can call Execute multiple times on a Query.
Note that using the ExecuteAsync is optional, as is hooking into QueryProgress events.


Query is interfaced with IQuery to help if you wish to use a test mock or an IoC container

Notes
-----

###License Stuff
Only because I have been told ( somewhat harshly ) that "dumping code into github isn't providing a license".  Sorry.  

This work is "AS IS" and without warranty of any kind, licensed under the [Creative Commons CC0 License](http://creativecommons.org/publicdomain/zero/1.0/)
