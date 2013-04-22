$(function() {
	App.Routing = function() {
		var Routing = {};

		Routing.Router = Backbone.Marionette.AppRouter.extend({
			appRoutes: {
				"": "actionIndex",
				"graph": "actionGraph"
			}
		});

		App.addInitializer(function() {
			Routing.router = new Routing.Router({
				controller: App.CyberNet
			});
			App.vent.trigger("routing:started");
		});

		return Routing;
	}();
});
