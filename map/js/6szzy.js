var school = 'szzy';
var sUserAgent = navigator.userAgent;

//UA是微信公众号的时候
if (sUserAgent.indexOf("MicroMessenger") == -1) {
    //return;
}


var map;
var the_host = "";

//数据存储的左、下、右、上的范围，默认为NULL
var mapBounds = new OpenLayers.Bounds(105.7286, 32.3124000003, 105.83504, 32.41782);

//重试加载图片3次后放弃加载，默认为0；
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
    map.setCenter(new OpenLayers.LonLat(105.75356, 32.33062), 2);
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
                    map.removePopup(popuplove);
            map.removePopup(popupwork);
            map.removePopup(popuplesson);
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
        //  map.removePopup(popup);
        //  map.removePopup(popuplove);
        //  map.removePopup(popupwork);
        //  map.removePopup(popuplesson);
        //  popup.destroy();
        //  popup = null;
        // }
        // popup = new OpenLayers.Popup.CSSFramedCloud(
        //  'transparentPopup_',
        //  new OpenLayers.LonLat(x+0.0001, y+0.000001),
        //  new OpenLayers.Size(50, 50),
        //  "<p><a href='" + "#" + "' style='color:#8b572a;text-shadow:0px 0px 4px rgba(0,0,0,0.5);'>" + bname + "</a></p>",
        //  null,
        //  false,
        //  onPopupClose,
        //  'tr'
        // );
        // popuplove = new OpenLayers.Popup.CSSFramedCloud(
        //  'transparentPopup_',
        //  new OpenLayers.LonLat(x+0.000599, y-0.0004),
        //  new OpenLayers.Size(50, 50),
        //  "<p><a href='" + "http://test.qifanhui.cn/zacao_demo/index/index_lovelist.html" + "' style='color:#4a4a4a;'>" + "表白" + "</a></p>",
        //  null,
        //  false,
        //  onPopupClose,
        //  'tr'
        // );
        // popupwork = new OpenLayers.Popup.CSSFramedCloud(
        //  'transparentPopup_',
        //  new OpenLayers.LonLat(x+0.0001, y-0.000901),
        //  new OpenLayers.Size(50, 50),
        //  "<p><a href='" + "http://test.qifanhui.cn/zacao_demo/copy/active.html" + "' style='color:#4a4a4a;'>" + "杂活" + "</a></p>",
        //  null,
        //  false,
        //  onPopupClose,
        //  'tr'
        // );
        // popuplesson = new OpenLayers.Popup.CSSFramedCloud(
        //  'transparentPopup_',
        //  new OpenLayers.LonLat(x-0.000401, y-0.0004),
        //  new OpenLayers.Size(50, 50),
        //  "<p><a href='" + "http://test.qifanhui.cn/zacao_demo/user/course.html" + "' style='color:#4a4a4a;'>" + "课程" + "</a></p>",
        //  null,
        //  false,
        //  onPopupClose,
        //  'tr'
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
//添加表白地标
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
    "var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75265, 32.32996), icon.clone()); " +
    "mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 76, '', '教工饭堂', 105.75265, 32.32996); });" +
    "markersLayer.addMarker(mark);" + 
    "var mark = new OpenLayers.Marker(new OpenLayers.LonLat(105.75106, 32.33362), icon.clone()); " +
    "mark.events.register('touchstart', mark, function(evt) { markerEvent(evt, 73, '', '雅志楼', 105.75106, 32.33362); });" + 
    "markersLayer.addMarker(mark);";
    showData(msg);
    
    $("#OL_Icon_56").append("<div class='actinfo'><div class='local'>雅致楼</div><i class='iconfontzacao'>&#xe600;</i> <span> <marquee scrollamount='3'>TO:小确幸 虽然你长得不帅，还很黑（简直黑出新高度了）但是，还是对你有那么一种特别的感觉，情绪会随着你的变化而变化，会总想找你聊天。爱你！我的暖宝宝！  #特别的爱给特别#</marquee></span></div>");
    $("#OL_Icon_50").append("<div class='actinfo'><div class='local'>雅致楼</div><i class='iconfontzacao'>&#xe600;</i> <span> <marquee scrollamount='3'>TO:小确幸 虽然你长得不帅，还很黑（简直黑出新高度了）但是，还是对你有那么一种特别的感觉，情绪会随着你的变化而变化，会总想找你聊天。爱你！我的暖宝宝！  #特别的爱给特别#</marquee></span></div>");

    $('#OL_Icon_52').append("<div class='actinfo'><div class='local'>教工饭堂</div><i class='iconfontzacao'>&#xe606;</i> <span> <marquee scrollamount='3'>我有一本小黄书 [地点 深职/深大/信息（全校）]</marquee></span></div>");
    $('#OL_Icon_46').append("<div class='actinfo'><div class='local'>教工饭堂</div><i class='iconfontzacao'>&#xe606;</i> <span> <marquee scrollamount='3'>我有一本小黄书 [地点 深职/深大/信息（全校）]</marquee></span></div>");
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
    }
    else link = "chat_room.aspx?id=" + id;
    if (popup != null) {
        map.removePopup(popup);
        popup.destroy();
        popup = null;
    }
    popup = new OpenLayers.Popup.CSSFramedCloud(
        'transparentPopup_',
        new OpenLayers.LonLat(x, y),
        new OpenLayers.Size(50, 50),
        "<p><a href='" + link + "'>" + bname + "</a></p>",
        null,
        false,
        onPopupClose,
        'tr'
    );
    // popuplove = new OpenLayers.Popup.CSSFramedCloud(
    //  'transparentPopup_',
    //  new OpenLayers.LonLat(x+0.0013, y-0.0004),
    //  new OpenLayers.Size(50, 50),
    //  "<p><a href='" + link + "'>" + "表白" + "</a></p>",
    //  null,
    //  false,
    //  onPopupClose,
    //  'tr'
    // );
    // popupwork = new OpenLayers.Popup.CSSFramedCloud(
    //  'transparentPopup_',
    //  new OpenLayers.LonLat(x+0.0013, y-0.0011),
    //  new OpenLayers.Size(50, 50),
    //  "<p><a href='" + link + "'>" + "杂活" + "</a></p>",
    //  null,
    //  false,
    //  onPopupClose,
    //  'tr'
    // );
    // popuplesson = new OpenLayers.Popup.CSSFramedCloud(
    //  'transparentPopup_',
    //  new OpenLayers.LonLat(x+0.0003, y-0.0015),
    //  new OpenLayers.Size(50, 50),
    //  "<p><a href='" + link + "'>" + "课程" + "</a></p>",
    //  null,
    //  false,
    //  onPopupClose,
    //  'tr'
    // );
    map.addPopup(popup);
    // map.addPopup(popuplove);
    // map.addPopup(popupwork);
    // map.addPopup(popuplesson);
}