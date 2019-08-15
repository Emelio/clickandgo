import 'package:flutter/material.dart';

class Destination extends StatefulWidget {
  @override
  _DestinationState createState() => _DestinationState();
}

class _DestinationState extends State<Destination> {
  Widget appBarTitle = new Text("AppBar Title");
  Icon actionIcon = new Icon(Icons.search);
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title:new TextField(
                        style: new TextStyle(
                          color: Colors.black,

                        ),
                        decoration: new InputDecoration(
                          prefixIcon: new Icon(Icons.search,color: Colors.white),
                          hintText: "Search...",
                          hintStyle: new TextStyle(color: Colors.white)
                        ),
                      ) ,
        backgroundColor: Colors.black26,
        actions: <Widget>[
         Row(
           children: <Widget>[
             new Icon(Icons.menu), 
                                   
 
           ],
         ),
        
         ],
      ),
    );
  }
}