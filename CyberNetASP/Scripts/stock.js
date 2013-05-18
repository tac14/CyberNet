$(function () {
	window.App = {
		Models: {},
        Views: {},
        Collections: {},
	};

	App.template = function (id) {
	    return Handlebars.compile($('#' + id).html());
	};

    App.Models.Item = Backbone.Model.extend({
        defaults: {
            name: "",
            type: "start",
			quality: 0.0,
			count: 0
        }
    });
	
    App.Collections.Items = Backbone.Collection.extend({ 
		model: App.Models.Item,
		url: 'StockLogic.aspx'		
	});

    App.Views.Item = Backbone.View.extend({
        tagName: 'tr',

        //templates: _.template( $('#list').html() ),
        templates: App.template('list'),

        render: function () {
			$(this.el).html(this.templates(this.model.toJSON()));
			return this;
        }
    });
 
    App.Views.Items = Backbone.View.extend({
        tagName: 'table',
		
		initialize: function() {
			this.collection.on('add', this.addOne, this);   
		},
		
        render: function() {
            this.collection.each(this.addOne, this);
            return this;
        },
		
        addOne: function(item) {
			debugger;
            // создавать новый дочерний вид
            var itemV = new App.Views.Item({ model: item });
            // добавлять его в корневой элемент
            $(this.el).append(itemV.render().el);
        }
    });

	var itemsCollection = new App.Collections.Items();
	
	var successFetch = function (collection, response) {
	    debugger;
		for (var item in response) {
			console.log(response[item]);
			collection.add(response[item]);
		}
	};
	
	var failFetch = function (collection, response) {
	    if (response.status == 401) {
	        document.location.href = '/Account/Login.aspx';
	    } else {
	        $('.block').html(App.template('error'));
	    }

	};
	
	
	var view = new App.Views.Items({ collection: itemsCollection});
 
    $('.block').html(view.render().el);

    
	itemsCollection.fetch( { 
		success: successFetch,
		error: failFetch
	} );
	
	
	
});