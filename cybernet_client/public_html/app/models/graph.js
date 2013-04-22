var Graph = Backbone.Model.extend({
	url : function () {
		return URLs.graph + '/' + this.id;
	}
});