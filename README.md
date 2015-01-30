RavenStuff
==========

a number of RavenDB spikes, packaged into one solution

* RavenDocument - class(es) used by the other projects
* RMDB - an MVC app which uses everything else. 

Once this project is more mature, these projects will go away and all functionality will be in the RMDB MVC App: 
* WriteToRaven - command-line project to create Movie documents and each movie's associated Actors //TODO: have it read from a file rather than having to edit source for each new movie.
* ReadFromRaven - reads a document & dumps to command window. 
* EditMovie - a simple spike which reads from Raven, changes the object, then writes back. // this may be obsolete given changes to the data model.




