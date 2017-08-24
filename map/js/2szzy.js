var school = 'szzy';
var sUserAgent = navigator.userAgent;
if (sUserAgent.indexOf("MicroMessenger") == -1) {//window.location.href='menu.aspx';     
	//return;
}
var map;
var the_host = "";
var mapBounds = new OpenLayers.Bounds(105.7286, 32.3124000003, 105.83504, 32.41782);
OpenLayers.IMAGE_RELOAD_ATTEMPTS = 3;
OpenLayers.Popup.CSSFramedCloud = OpenLayers.Class(OpenLayers.Popup.Framed, {

	autoSize: true,

	panMapIfOutOfView: true,

	fixedRelativePosition: false,

 

  positionBlocks: {

		"tl": {

			'offset': new OpenLayers.Pixel(44, -6),

			'padding': new OpenLayers.Bounds(5, 14, 5, 5),

			'blocks': [

				{

					className: 'olwidgetPopupStemTL',

					size: new OpenLayers.Size(20, 20),

					anchor: new OpenLayers.Bounds(null, 4, 32, null),

					position: new OpenLayers.Pixel(0, -28)

				}

			]

		},

		"tr": {

			'offset': new OpenLayers.Pixel(-44, -6),

			'padding': new OpenLayers.Bounds(5, 14, 5, 5),

			'blocks': [

				{

					className: "olwidgetPopupStemTR",

					size: new OpenLayers.Size(10, 10),

					anchor: new OpenLayers.Bounds(32, 9, null, null),

					position: new OpenLayers.Pixel(0, -28)

				}

			]

		},

		"bl": {

			'offset': new OpenLayers.Pixel(44, 6),

			'padding': new OpenLayers.Bounds(5, 5, 5, 14),

			'blocks': [

				{

					className: "olwidgetPopupStemBL",

					size: new OpenLayers.Size(20,20),

					anchor: new OpenLayers.Bounds(null, null, 32, 4),

					position: new OpenLayers.Pixel(0, 0)

				}

			]

		},

		"br": {

			'offset': new OpenLayers.Pixel(-44, 6),

			'padding': new OpenLayers.Bounds(5, 5, 5, 14),

			'blocks': [

				{

					className: "olwidgetPopupStemBR",

					size: new OpenLayers.Size(20, 20),

					anchor: new OpenLayers.Bounds(32, null, null, 4),

					position: new OpenLayers.Pixel(0, 0)

				}

			]

		}

	},

 

	initialize: function(id, lonlat, contentSize, contentHTML, anchor, closeBox,

					closeBoxCallback, relativePosition, separator) {

		if (relativePosition && relativePosition != 'auto') {

			this.fixedRelativePosition = true;

			this.relativePosition = relativePosition;

		}

		if (separator === undefined) {

			this.separator = ' of ';

		} else {

			this.separator = separator;

		}

 

		this.olwidgetCloseBox = closeBox;

		this.olwidgetCloseBoxCallback = closeBoxCallback;

		this.page = 0;

		OpenLayers.Popup.Framed.prototype.initialize.apply(this, [id, lonlat,

			contentSize, contentHTML, anchor, false, null]);

	},

 

	/*

	 * 构造popup内部容器。

	 */

	setContentHTML: function(contentHTML) {

		if (contentHTML !== null && contentHTML !== undefined) {

			this.contentHTML = contentHTML;

		}

   

		if (this.contentDiv !== null)  {

			var popup = this;

 

			// 清空旧数据

			this.contentDiv.innerHTML = "";

 

			// 创建内部容器

			var containerDiv = document.createElement("div");

			containerDiv.innerHTML = this.contentHTML;

			containerDiv.className = 'olwidgetPopupContent';

			this.contentDiv.appendChild(containerDiv);

 

			// 创建关闭按钮

			if (this.olwidgetCloseBox) {

				var closeDiv = document.createElement("div");

				closeDiv.className = "olwidgetPopupCloseBox";

				closeDiv.innerHTML = "close";

				closeDiv.onclick = function(event) {

					popup.olwidgetCloseBoxCallback.apply(popup, arguments);

				};

				this.contentDiv.appendChild(closeDiv);

			}

			if (this.autoSize) {

				this.registerImageListeners();

				this.updateSize();

			}

		}

	},

 

	/*

	 * 重写createBlocks：使用CSS样式而不是特定的img图片

	 */

	createBlocks: function() {

		this.blocks = [];

 

		// since all positions contain the same number of blocks, we can

		// just pick the first position and use its blocks array to create

		// our blocks array

		var firstPosition = null;

		for(var key in this.positionBlocks) {

			firstPosition = key;

			break;

		}

 

		var position = this.positionBlocks[firstPosition];

		for (var i = 0; i < position.blocks.length; i++) {

 

			var block = {};

			this.blocks.push(block);

 

			var divId = this.id + '_FrameDecorationDiv_' + i;

			block.div = OpenLayers.Util.createDiv(divId,

				null, null, null, "absolute", null, "hidden", null

			);

			this.groupDiv.appendChild(block.div);

		}

	},

	/*

	 * 重写updateBlocks

	 */

	updateBlocks: function() {

		if (!this.blocks) {

			this.createBlocks();

		}

		if (this.size && this.relativePosition) {

			var position = this.positionBlocks[this.relativePosition];

			for (var i = 0; i < position.blocks.length; i++) {

 

				var positionBlock = position.blocks[i];

				var block = this.blocks[i];

 

				// adjust sizes

				var l = positionBlock.anchor.left;

				var b = positionBlock.anchor.bottom;

				var r = positionBlock.anchor.right;

				var t = positionBlock.anchor.top;

 

				// note that we use the isNaN() test here because if the

				// size object is initialized with a "auto" parameter, the

				// size constructor calls parseFloat() on the string,

				// which will turn it into NaN

				//

				var w = (isNaN(positionBlock.size.w)) ? this.size.w - (r + l)

													  : positionBlock.size.w;

 

				var h = (isNaN(positionBlock.size.h)) ? this.size.h - (b + t)

													  : positionBlock.size.h;

 

				block.div.style.width = (w < 0 ? 0 : w) + 'px';

				block.div.style.height = (h < 0 ? 0 : h) + 'px';

 

				block.div.style.left = (l !== null) ? l + 'px' : '';

				block.div.style.bottom = (b !== null) ? b + 'px' : '';

				block.div.style.right = (r !== null) ? r + 'px' : '';

				block.div.style.top = (t !== null) ? t + 'px' : '';

 

				block.div.className = positionBlock.className;

			}

 

			this.contentDiv.style.left = this.padding.left + "px";

			this.contentDiv.style.top = this.padding.top + "px";

		}

	},

	updateSize: function() {

		 

			return OpenLayers.Popup.prototype.updateSize.apply(this, arguments);

	 

	},

 

	CLASS_NAME: "OpenLayers.Popup.CSSFramedCloud"

});

var popup;
//生成地图
function init() {
	var mapOptions = {
		controls: [
			new OpenLayers.Control.Navigation({
				dragPanOptions: {
					enableKinetic: true
				}
			})
		],
		maxExtent: new OpenLayers.Bounds(105.7286, 32.3124000003, 105.76470, 32.34859),
		maxResolution: 0.00007072912960,
		numZoomLevels: 4,
		restrictedExtent: new OpenLayers.Bounds(105.7286, 32.3124000003, 105.76470, 32.34859),
		units: "degrees"
	};
	map = new OpenLayers.Map('map', mapOptions);
	var baseLayer = new OpenLayers.Layer.TMS("基础图层", "", {
		url: '',
		serviceVersion: '1.0',
		layername: 'basic',
		alpha: true,
		type: 'jpg',
		isBaseLayer: true,
		getURL: overlay_getTileURL
	});
	map.addLayer(baseLayer);
	map.zoomToExtent(mapBounds);
	map.events.register("zoomend", map, changeMark);
	map.setCenter(new OpenLayers.LonLat(105.75356, 32.32662), 2);
	vectorLayer = new OpenLayers.Layer.Vector("Vector Layer");
	map.addLayers([vectorLayer]);
	markersLayer = new OpenLayers.Layer.Markers("Markers");
	map.addLayers([markersLayer])
	function overlay_getTileURL(bounds) {
		var res = this.map.getResolution();
		var x = Math.round((bounds.left - this.maxExtent.left) / (res * this.tileSize.w));
		var y = Math.round((bounds.bottom - this.maxExtent.bottom) / (res * this.tileSize.h));
		var z = this.map.getZoom();
		if (x >= 0 && y >= 0) {
			return the_host + "map/" + school + "/" + z + "/" + x + "-" + y + "." + this.type;
		} else {
			return the_host + "map/" + school + "/nono.png";
		}
	}
	init2();
}
function onMapClick(e) {
	// 显示地图屏幕坐标
	if(popup != null) {
		map.removePopup(popup);
		// map.removePopup(popuplove);
		// map.removePopup(popupwork);
		// map.removePopup(popuplesson);
		popup.destroy();
		popup = null;
	}
	$(".map-sm").hide();
}
var ii = 0;
//改变缩放时显示对应的界面
function changeMark(evt) {
	if(ii == 0) {
		ii = 1;
		return;
	}
	var i = parseInt(map.getZoom());
	displayMark(i);
}
//停止事件
function onPopupClose(evt) {
	//selectControl.unselectAll();
	OpenLayers.Event.stop(evt); //停止事件
}
var link;

//显示对应缩放地图界面显示，否则隐藏
function displayMark(i) {
	if(i == 0 || i == 1) {
		for(var i = 0; i < markersLayer.markers.length; i++) {
			markersLayer.markers[i].display();
		}
	} else {
		addMark();
		for(var i = 0; i < markersLayer.markers.length; i++) {
			markersLayer.markers[i].display("block");
		}
	}
}

function addPop(id, web, bname, x, y) {
	if(web != "" && web.length > 5) {
		if(web.indexOf("//") == -1)
			web = "http://" + web;
		window.location.href = web;
	} else {
		// if(popup != null) {
		// 	map.removePopup(popup);
		// 	// map.removePopup(popuplove);
		// 	// map.removePopup(popupwork);
		// 	// map.removePopup(popuplesson);
		// 	popup.destroy();
		// 	popup = null;
		// }
		//      link = "chat_room.aspx?id=" + id;
		//link = "http://www.baidu.com";
		// popup = new OpenLayers.Popup.CSSFramedCloud(
		// 	'transparentPopup_',
		// 	new OpenLayers.LonLat(x+0.0001, y+0.000001),
		// 	new OpenLayers.Size(50, 50),
		// 	"<p><a href='" + "#" + "' style='color:#8b572a;text-shadow:0px 0px 4px rgba(0,0,0,0.5);'>" + bname + "</a></p>",
		// 	null,
		// 	false,
		// 	onPopupClose,
		// 	'tr'
		// );
		// popuplove = new OpenLayers.Popup.CSSFramedCloud(
		// 	'transparentPopup_',
		// 	new OpenLayers.LonLat(x+0.000599, y-0.0004),
		// 	new OpenLayers.Size(50, 50),
		// 	"<p><a href='" + "http://test.qifanhui.cn/zacao_demo/index/index_lovelist.html" + "' style='color:#4a4a4a;'>" + "表白" + "</a></p>",
		// 	null,
		// 	false,
		// 	onPopupClose,
		// 	'tr'
		// );
		// popupwork = new OpenLayers.Popup.CSSFramedCloud(
		// 	'transparentPopup_',
		// 	new OpenLayers.LonLat(x+0.0001, y-0.000901),
		// 	new OpenLayers.Size(50, 50),
		// 	"<p><a href='" + "http://test.qifanhui.cn/zacao_demo/copy/active.html" + "' style='color:#4a4a4a;'>" + "杂活" + "</a></p>",
		// 	null,
		// 	false,
		// 	onPopupClose,
		// 	'tr'
		// );
		// popuplesson = new OpenLayers.Popup.CSSFramedCloud(
		// 	'transparentPopup_',
		// 	new OpenLayers.LonLat(x-0.000401, y-0.0004),
		// 	new OpenLayers.Size(50, 50),
		// 	"<p><a href='" + "http://test.qifanhui.cn/zacao_demo/user/course.html" + "' style='color:#4a4a4a;'>" + "课程" + "</a></p>",
		// 	null,
		// 	false,
		// 	onPopupClose,
		// 	'tr'
		// );
		// map.addPopup(popup);
		// map.addPopup(popuplove);
		// map.addPopup(popupwork);
		// map.addPopup(popuplesson);
	}
}

function markerEvent(evt, id, web, bname, x, y) {
	OpenLayers.Event.stop(evt); //停止事件
	addPop(id, web, bname, x, y);
}
//添加地标
function showData(obj) {
	if(obj != "") {
		var objData = obj;
		var iconUrl = "img/seat1.png"; //图片地址
		var size = new OpenLayers.Size(30, 30);
		var offset = new OpenLayers.Pixel(-(size.w / 2), -size.h + 10);
		var icon = new OpenLayers.Icon(iconUrl, size, offset);
		eval(obj);
		addMark();

	}
}
//生成地标
function init2() {
	var msg = "" +
	// "var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75702, 32.32552), icon.clone());" +
	// "mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 60, '', '大运广场', 105.75702, 32.32552); });" +
	// "markersLayer.addMarker(mark);";

	// "var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75265, 32.32996), icon.clone()); " +
	// "mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 76, '', '教工饭堂', 105.75265, 32.32996); });" +
	// "markersLayer.addMarker(mark);" ;

	// var msg = "var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75902, 32.32519), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 190, '', '图书馆', 105.75902, 32.32519); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75348, 32.33532), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 66, '', '柳园F栋', 105.75348, 32.33532); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75516, 32.33459), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 67, '', '柳园A栋', 105.75516, 32.33459); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75679, 32.3341), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 68, '', '柳园B栋', 105.75679, 32.3341); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75636, 32.33344), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 69, '', '柳园C栋', 105.75636, 32.33344); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74993, 32.33537), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 70, '', '均园I栋', 105.74993, 32.33537); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75058, 32.33442), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 71, '', '均园H栋', 105.75058, 32.33442); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75053, 32.33392), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 72, '', '均园C栋', 105.75053, 32.33392); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75106, 32.33362), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 73, '', '雅志楼', 105.75106, 32.33362); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7518, 32.33245), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 74, '', '雅诗楼', 105.7518, 32.33245); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75176, 32.33154), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 75, '', '学术交流中心', 105.75176, 32.33154); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75265, 32.32996), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 76, '', '教工饭堂', 105.75265, 32.32996); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75802, 32.32816), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 77, '', '厚德楼', 105.75802, 32.32816); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7454, 32.33803), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 191, '', '报告厅', 105.7454, 32.33803); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74843, 32.33916), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 192, '', '食堂', 105.74843, 32.33916); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74243, 32.34019), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 193, '', '实训楼', 105.74243, 32.34019); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74392, 32.34067), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 194, '', '崇德楼', 105.74392, 32.34067); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74912, 32.34229), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 195, '', '一栋', 105.74912, 32.34229); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74675, 32.34159), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 196, '', '二栋', 105.74675, 32.34159); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74784, 32.34349), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 197, '', '三栋', 105.74784, 32.34349); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74987, 32.33701), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 51, '', '田径场', 105.74987, 32.33701); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75288, 32.33735), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 52, '', '体育馆主馆', 105.75288, 32.33735); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.755, 32.33595), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 53, '', '体育馆副馆', 105.755, 32.33595); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75309, 32.33491), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 54, '', '柳园E栋', 105.75309, 32.33491); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75637, 32.33185), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 56, '', '学生饭堂', 105.75637, 32.33185); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75437, 32.33569), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 57, '', '柳园G栋', 105.75437, 32.33569); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75608, 32.33491), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 58, '', '柳园D栋', 105.75608, 32.33491); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75446, 32.32183), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 59, '', '南门饭堂', 105.75446, 32.32183); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75702, 32.32552), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 60, '', '大运广场', 105.75702, 32.32552); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75808, 32.32036), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 61, '', '南门篮球场', 105.75808, 32.32036); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75157, 32.3254), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 62, '', '格物园A', 105.75157, 32.3254); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75145, 32.32393), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 63, '', '格物园B', 105.75145, 32.32393); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75636, 32.32367), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 64, '', '信息楼', 105.75636, 32.32367); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75299, 32.32657), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 78, '', '教学楼', 105.75299, 32.32657); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75385, 32.32761), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 79, '', '日新楼', 105.75385, 32.32761); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.76001, 32.32135), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 80, '', '羽毛球馆', 105.76001, 32.32135); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75663, 32.32457), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 81, '', '图书馆', 105.75663, 32.32457); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75662, 32.31957), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 83, '', '物流服务中心', 105.75662, 32.31957); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73456, 32.33098), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 163, '', '西区正门', 105.73456, 32.33098); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73981, 32.32801), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 164, '', '西区侧门', 105.73981, 32.32801); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73766, 32.33039), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 165, '', '溪湖', 105.73766, 32.33039); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73573, 32.33358), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 166, '', '德业楼', 105.73573, 32.33358); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73744, 32.33273), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 167, '', '学思楼', 105.73744, 32.33273); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73352, 32.33494), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 168, '', '明德楼', 105.73352, 32.33494); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7358, 32.33628), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 169, '', '知行园A', 105.7358, 32.33628); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.73889, 32.33488), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 170, '', '图书馆', 105.73889, 32.33488); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74121, 32.33389), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 171, '', '知行园B', 105.74121, 32.33389); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74358, 32.33294), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 172, '', '音乐厅', 105.74358, 32.33294); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74121, 32.33043), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 173, '', '运动场', 105.74121, 32.33043); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74151, 32.32831), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 174, '', '锦园-9', 105.74151, 32.32831); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74217, 32.3291), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 175, '', '锦园-10', 105.74217, 32.3291); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74266, 32.32979), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 176, '', '锦园-11', 105.74266, 32.32979); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74333, 32.33036), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 177, '', '锦园-12', 105.74333, 32.33036); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7448, 32.33243), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 178, '', '食堂-1', 105.7448, 32.33243); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7439, 32.32696), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 179, '', '锦园-1', 105.7439, 32.32696); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74468, 32.32772), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 180, '', '锦园-2', 105.74468, 32.32772); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.7454, 32.3285), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 181, '', '锦园-4', 105.7454, 32.3285); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.746, 32.32898), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 182, '', '锦园-3', 105.746, 32.32898); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74673, 32.32991), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 183, '', '锦园-6', 105.74673, 32.32991); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74761, 32.3306), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 184, '', '锦园-5', 105.74761, 32.3306); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74795, 32.33136), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 185, '', '锦园-7', 105.74795, 32.33136); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74851, 32.33186), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 186, '', '锦园-8', 105.74851, 32.33186); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.74687, 32.33234), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 187, '', '食堂-2', 105.74687, 32.33234); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75516, 32.31961), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 188, '', '东区南门', 105.75516, 32.31961); }); markersLayer.addMarker(mark); var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.76376, 32.32569), icon.clone()); mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 189, '', '东区正门', 105.76376, 32.32569); }); markersLayer.addMarker(mark);";
	showData(msg);
	// $('#OL_Icon_52').append("<div class='actinfo'><div class='local'>教工饭堂</div><i class='iconfontzacao'>&#xe606;</i> <span><marquee scrollamount='3'>我有一本小黄书 [地点 深职/深大/信息（全校）]</marquee></span></div>");
	// $('#OL_Icon_46').append("<div class='actinfo'><div class='local'>教工饭堂</div><i class='iconfontzacao'>&#xe606;</i> <span><marquee scrollamount='3'>我有一本小黄书 [地点 深职/深大/信息（全校）]</marquee></span></div>");
}

function addMark() {
	var i = parseInt(map.getZoom());
	var s = 1;
	if(i == 0)
		s = 4;
	else if(i == 1)
		s = 3;
	else if(i == 2)
		s = 2;
	var iconUrl3, size3, icons;
}

function panto2(id, web, bname, x, y) {
  map.setCenter(new OpenLayers.LonLat(x, y), map.getZoom());
  addPop2(id, web, bname,x,y);
  $("#closebtn").click();
}

function addPop2(id, web, bname, x, y) {
	if (web != "" && web.length > 5) {
		if (web.indexOf("//") == -1)
		web = "http://" + web;
		//window.location.href = web;
	}else link = "chat_room.aspx?id=" + id;
	if (popup != null) {
		map.removePopup(popup);
		popup.destroy();
		popup = null;
	}
	// popup = new OpenLayers.Popup.CSSFramedCloud(
	// 	'transparentPopup_',
	// 	new OpenLayers.LonLat(x, y),
	// 	new OpenLayers.Size(50, 50),
	// 	"<p><a href='" + link + "'>" + bname + "</a></p>",
	// 	null,
	// 	false,
	// 	onPopupClose,
	// 	'tr'
	// );
	// popuplove = new OpenLayers.Popup.CSSFramedCloud(
	// 	'transparentPopup_',
	// 	new OpenLayers.LonLat(x+0.0013, y-0.0004),
	// 	new OpenLayers.Size(50, 50),
	// 	"<p><a href='" + link + "'>" + "表白" + "</a></p>",
	// 	null,
	// 	false,
	// 	onPopupClose,
	// 	'tr'
	// );
	// popupwork = new OpenLayers.Popup.CSSFramedCloud(
	// 	'transparentPopup_',
	// 	new OpenLayers.LonLat(x+0.0013, y-0.0011),
	// 	new OpenLayers.Size(50, 50),
	// 	"<p><a href='" + link + "'>" + "杂活" + "</a></p>",
	// 	null,
	// 	false,
	// 	onPopupClose,
	// 	'tr'
	// );
	// popuplesson = new OpenLayers.Popup.CSSFramedCloud(
	// 	'transparentPopup_',
	// 	new OpenLayers.LonLat(x+0.0003, y-0.0015),
	// 	new OpenLayers.Size(50, 50),
	// 	"<p><a href='" + link + "'>" + "课程" + "</a></p>",
	// 	null,
	// 	false,
	// 	onPopupClose,
	// 	'tr'
	// );
	// map.addPopup(popup);
	// map.addPopup(popuplove);
	// map.addPopup(popupwork);
	// map.addPopup(popuplesson);
}
