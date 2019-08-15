import 'package:flutter/material.dart';

class VerifyCode extends StatefulWidget {
  @override
  _VerifyCodeState createState() => _VerifyCodeState();
}

class _VerifyCodeState extends State<VerifyCode> {
  final _formKey = GlobalKey<FormState>();
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("images/component.png"),
            fit: BoxFit.cover,
          ),
        ),
        child: Form(
          key: _formKey,
          child: Padding(
            padding: const EdgeInsets.only(top: 100, left: 10, right: 10),
            child: Column(
              children: <Widget>[
                Text(
                  "Please check your email for the verification code",
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 19.9,
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.only(top: 50),
                  child: TextFormField(
                    decoration: InputDecoration(
                      labelText: "Code",
                      border: OutlineInputBorder(
                        gapPadding: 3.3,
                        borderRadius: BorderRadius.circular(30),
                      ),
                      labelStyle: TextStyle(color: Colors.yellow),
                    ),
                    style: TextStyle(color: Colors.yellow),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
