var http = require('http');
var express = require('express');
var app = express();
var port = process.env.PORT || 4000;
app.use('/', express.static('client/'));

app.use(function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});


// ~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~

app.route('/*').get(function(req, res) {
	res.sendFile('index.html', {root:"client/"})
});

app.listen(port);
console.log(port + " people came tonight!");