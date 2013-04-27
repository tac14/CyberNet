

$(function() {


	App = new Backbone.Marionette.Application();
	App.CyberNet = function() {

		var CyberNet = {};
		var GraphLayout = Backbone.Marionette.Layout.extend({
			template: "#graph-layout-template",
			regions: {
				graph: '#graph'
			}
		});
		CyberNet.actionIndex = function() {
			var v = new MainView();
			App.content.show(v);
			console.log('index');
		};
		CyberNet.actionGraph = function() {

			var graph = new Graph();



			console.log("graph");
			var l = new GraphLayout();
			App.content.show(l);

			graph.id = 1;
			var view = new GraphView();
			view.on('show', function() {

			});
//			graph.fetch({
//				success: function() {
			l.graph.show(view);

			$("#draggable").draggable();

			$('#infovis').droppable({
				accept: "#draggable"
						,
				drop: function(event, ui) {

					var jsonnode = {
						"id": 'n4',
						"name": "N4",
						"data": {
							"some other key": "some other value"
						}
					};
					fd.graph.addNode(jsonnode);
					var dataset = {
						"data": {
						}};
//					fd.graph.addAdjacence(fd.graph.getNode
//							(ui.draggable.attr("id")), fd.graph.getNode("n1"), dataset);

					//compute positions and plot 
					fd.graph.eachNode(function(node) {
						var n = node.id.substring(1);
						node.pos.x = -600 + 300 * n;
						node.pos.y = 0;
					});
					fd.plot();
					ui.draggable.remove();
				}
			});

			var json = [
				{
					"id": "n1",
					"name": "N1",
					"data": {
					},
					"adjacencies": [
						{
							'nodeFrom': 'n1',
							'nodeTo': 'n2',
							'data': {
								'labeltext': 'AA'
							}
						}
					]
				},
				{
					"id": "n2",
					"name": "N2",
					"data": {
					},
					"adjacencies": [
						{
							'nodeFrom': 'n2',
							'nodeTo': 'n3',
							'data': {
								'labeltext': 'AA2'
							}
						}
					]
				},
				{
					"id": "n3",
					"name": "N3",
					"data": {
					},
					"adjacencies": [
					]
				},
			];
			$jit.ForceDirected.Plot.EdgeTypes.implement({
				'label-arrow-line': {
					'render': function(adj, canvas) {
						var from = adj.nodeFrom.pos.getc(true),
								to = adj.nodeTo.pos.getc(true),
								dim = adj.getData('dim'),
								direction = adj.data.$direction,
								inv = (direction && direction.length > 1 && direction[0] != adj.nodeFrom.id);
						this.edgeHelper.arrow.render(from, to, dim, inv, canvas);
						var pos = adj.nodeFrom.pos.getc(true);
						var posChild = adj.nodeTo.pos.getc(true);
//check for edge label in data
						var data = adj.data;
						if (data.labeltext) {
							//now adjust the label placement
							var radius = this.viz.canvas.getSize();
							var x = parseInt((pos.x + posChild.x - (data.labeltext.length * 5)) /
									2);
							var y = parseInt((pos.y + posChild.y) / 2);
							var ctx = canvas.getCtx();
							var prev = ctx.font;
							ctx.font = '18px Arial black';
							ctx.fillStyle = 'black';
							ctx.fillText(data.labeltext, x, y);
							ctx.font = prev;
						}
					},
					'contains'
							: function(adj, pos) {
						var from = adj.nodeFrom.pos.getc(true),
								to = adj.nodeTo.pos.getc(true);
						return this.edgeHelper.arrow.contains(from, to, pos, this.edge.epsilon);
					}
				}
			});
			var fd = new $jit.ForceDirected({
//id of the visualization container  
				injectInto: 'infovis',
				//Enable zooming and panning  
				//with scrolling and DnD  
				Navigation: {
					enable: true,
					type: 'Native',
					//Enable panning events only if we're dragging the empty  
					//canvas (and not a node).  
					panning: 'avoid nodes',
					zooming: 100 //zoom speed. higher is more sensible  
				},
				// Change node and edge styles such as  
				// color and width.  
				// These properties are also set per node  
				// with dollar prefixed data-properties in the  
				// JSON structure.  
				Node: {
					overridable: true,
					type: 'rectangle',
					width: 100,
					height: 40
				},
				NodeStyles: {
					enable: true,
					stylesHover: {
						dim: 30,
						color: '#fcc'
					},
					duration: 600
				},
				Edge: {
					color: '#23A4FF',
					lineWidth: 4,
					dim: 40,
					type: 'label-arrow-line',
//						type: 'arrow',
					overridable: true,
					direction: ['nodeFrom', 'nodeTo']
				},
				// Add node events  
				Events: {
					enable: true,
					type: 'Native',
					//Change cursor style when hovering a node  
					onMouseEnter: function() {
						fd.canvas.getElement().style.cursor = 'move';
					},
					onMouseLeave: function() {
						fd.canvas.getElement().style.cursor = '';
					},
					//Update node positions when dragged  
					onDragMove: function(node, eventInfo, e) {
						var pos = eventInfo.getPos();
						node.pos.setc(pos.x, pos.y);
						fd.plot();
					},
					//Implement the same handler for touchscreens  
					onTouchMove: function(node, eventInfo, e) {
						$jit.util.event.stop(e); //stop default touchmove event  
						this.onDragMove(node, eventInfo, e);
					}
				},
				//Number of iterations for the FD algorithm  
				iterations: 200,
				//Edge length  
				levelDistance: 200,
				// This method is only triggered  
				// on label creation and only for DOM labels (not native canvas ones).  
				onCreateLabel: function(domElement, node) {
// Create a 'name' and 'close' buttons and add them  
// to the main node label  
					var nameContainer = document.createElement('span'),
							closeButton = document.createElement('span'),
							style = nameContainer.style;
					nameContainer.className = 'name';
					nameContainer.innerHTML = node.name;
					closeButton.className = 'close';
					closeButton.innerHTML = 'x';
					domElement.appendChild(nameContainer);
//						domElement.appendChild(closeButton);
					style.fontSize = "1.2em";
					style.color = "#000";
					//Fade the node and its connections when  
					//clicking the close button  
					closeButton.onclick = function() {
						node.setData('alpha', 0, 'end');
						node.eachAdjacency(function(adj) {
							adj.setData('alpha', 0, 'end');
						});
						fd.fx.animate({
							modes: ['node-property:alpha',
								'edge-property:alpha'],
							duration: 500
						});
					};
					//Toggle a node selection when clicking  
					//its name. This is done by animating some  
					//node styles like its dimension and the color  
					//and lineWidth of its adjacencies.  
					nameContainer.onclick = function() {
//set final styles  
						fd.graph.eachNode(function(n) {
							if (n.id != node.id)
								delete n.selected;
							n.setData('dim', 7, 'end');
							n.eachAdjacency(function(adj) {
								adj.setDataset('end', {
									lineWidth: 0.4,
									color: '#23a4ff'
								});
							});
						});
						if (!node.selected) {
							node.selected = true;
							node.setData('dim', 17, 'end');
							node.eachAdjacency(function(adj) {
								adj.setDataset('end', {
									lineWidth: 3,
									color: '#36acfb'
								});
							});
						} else {
							delete node.selected;
						}
//trigger animation to final styles  
						fd.fx.animate({
							modes: ['node-property:dim',
								'edge-property:lineWidth:color'],
							duration: 500
						});
						// Build the right column relations list.  
						// This is done by traversing the clicked node connections.  
						var html = "<h4>" + node.name + "</h4><b> connections:</b><ul><li>",
								list = [];
						node.eachAdjacency(function(adj) {
							if (adj.getData('alpha'))
								list.push(adj.nodeTo.name);
						});
						//append connections information  
//							$jit.id('inner-details').innerHTML = html + list.join("</li><li>") + "</li></ul>";
					};
				},
				// Change node styles when DOM labels are placed  
				// or moved.  
				onPlaceLabel: function(domElement, node) {
					var style = domElement.style;
					var left = parseInt(style.left);
					var top = parseInt(style.top);
					var w = domElement.offsetWidth;
					var h = domElement.offsetHeight;
					style.left = (left - w / 2) + 'px';
					style.top = (top + 25) + 'px';
					style.display = '';
				}
			});
// load JSON data.  
			fd.loadJSON(json);
// compute positions incrementally and animate.  
			fd.computeIncremental({
				iter: 40,
				property: 'end',
				onStep: function(perc) {
//						Log.write(perc + '% loaded...');
				},
				onComplete: function() {
//						Log.write('done');
					fd.graph.eachNode(function(node) {
						var n = node.id.substring(1);
						node.pos.x = -600 + 300 * n;
						node.pos.y = 0;
					});
					fd.plot();
				}
			});


//				}
//			});
		};
		return CyberNet;
	}();
	App.addRegions({
		content: '#container'
	});
	App.addInitializer(function() {
		console.log('init');
	});
	App.vent.on("routing:started", function() {
		if (!Backbone.History.started)
			Backbone.history.start();
	});
	App.start();
});