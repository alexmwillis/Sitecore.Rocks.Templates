var fs = require("fs");
var Handlebars = require('Handlebars');
var source  = fs.readFileSync("./template.html", "utf8");
var template = Handlebars.compile(source);
var context = {
  title: "First Post",
  story: {
    intro: "Before the jump",
    body: "After the jump"
  }
};
console.log(template(context));
