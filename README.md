Sitecore.Rocks.Templates
==============

A [Sitecore.Rocks](http://vsplugins.sitecore.net/) plugin for generating text from Sitecore items. 

The plugin is aimed at generating source code for Sitecore projects [Minq](https://github.com/tcuk/minq) and [Sinj](https://github.com/tcuk/sinj). Text is generated using a [fork](https://github.com/alexmwillis/Handlebars.Net) of the [Handlebars.Net](https://github.com/rexm/Handlebars.Net) templating engine, so templates are fully configurable.


##Prerequisites

* Sitecore.Rocks

##Install

* Build project. 
* The plugin will installed to `%localappdata%\Sitecore\Sitecore.Rocks\Plugins`

##Usage

* In the Visual Studio Sitecore Explorer, right click an item or template and select 'Copy to template...'
* Select a template from drop-down list.
* The clipboard will be populated with the formatted text.

###Creating new templates

The project comes with templates for generating JSON objects (Sinj) and C# MVC models (Minq). However the text that is generated is fully configurable. To add a new template, simply create a .hbs file and add it to one of the following locations under the Sitecore.Rocks plugin folder.

* **Resources\Item Templates** - templates added here will appear in the drop-down menu when a Sitecore item is selected.
* **Resources\Template Templates**  -templates added here will appear in the drop-down menu when a Sitecore template is selected.
* **Resources\Partials**  - templates added here will be registered as partials. See Handlebars.Net documentation on partials.

###Example template

`simpleJson.hbs`

```javascript
var {{camelCase Name}}{{pascalCase TemplateName}} = { 
    id: "{{Id}}",
    name: "{{Name}}",
    template: "{{TemplatePath}}",
    parent: "{{ParentPath}}",
    language: "en",
{{#if Fields}}
    fields: {
        "{{Name}}": "{{literal Value}}"{{#unless @last}},
		 {{/unless}}
    }
{{else}}
    fields: {}
{{/if}}
};
```

Given a Sitecore item called 'New' with template 'Web Component', `simpleJson.hbs` would render:

```javascript
var newWebComponent = { 
    id: "00000000-0000-0000-0000-000000000000",
    name: "New",
    template: "/template/path",
    parent: "/parent/path",
    language: "en",
    fields: {
        "Field Name 1": "Field Value 1",
        "Field Name 2": "Field Value 2"
    }
}
```

###Helpers
The template in this example above makes use of the following built in helpers

* **pascalCase** - takes a string as parameter then outputs the string with capitalised words. Spaces and special characters are removed.
* **camelCase** - like pascalCase, but with a lowercase first letter.
* **literal** - takes a string as parameter then outputs the string with escaped special characters.

See Handlebars.Net documentation for following helpers

* **#unless**
* **#if**

Also see unit tests for further examples of Helpers.