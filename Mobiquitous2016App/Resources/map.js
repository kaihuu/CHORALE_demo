var map;							//! マップ オブジェクト。
var geo;							//! Geo コード取得用オブジェクト。
var isInitialized = false;			//! 初期化フラグ。
var circles = [];	//! マーカーのコレクション。
var imageMarkers = [];
var nextID = 0;				//! 次に割り当てられるマーカーの識別子。
var selectedID = -1;				//! 選択されているマーカーの識別子。

var currentCircle;

var yellow = "#ffff00";
var red = "#ff0000";
var pink = "#da70d6";
var black = "#000";

// Marker に id プロパティを追加 ( 初期値は無効値 )
google.maps.Marker.prototype.id = -1;

/**
 * マップの初期化を行います。
 */
function initialize() {
    isInitialized = false;
    var mapdiv = document.getElementById("map_canvas");
    var myOptions = { zoom: 16, center: new google.maps.LatLng(35.473695, 139.590859), mapTypeId: google.maps.MapTypeId.ROADMAP, scaleControl: true };
    map = new google.maps.Map(mapdiv, myOptions);
    geo = new google.maps.Geocoder();

    // マップの中央位置が更新された時のイベント
    /*google.maps.event.addListener(map, 'center_changed', function () {
        
    });
    */

    // マップのタイル読み込みが完了した時のイベント
    google.maps.event.addListener(map, 'tilesloaded', function () {
        // 起動時に一度だけ座標を更新する
        if (!isInitialized) {
            isInitialized = true;
            window.external.OnInitCompleted();
        }
    });
}

function getColor(number) {

    switch (number) {
        case 188:
            return pink;
        case 189:
            return red;
        case 190:
            return red;
        case 191:
            return red;
        case 192:
            return red;
        case 193:
            return pink;
        case 194:
            return pink;
        case 195:
            return pink;
        case 196:
            return pink;
        case 197:
            return red;
        case 198:
            return red;
        case 199:
            return yellow;
        default:
            return black;
    }
}

function addLine(semanticLinkID, latitude1, longitude1, latitude2, longitude2) {

    // ラインを引く座標の配列を作成 
    var mapPoints = [
        new google.maps.LatLng(latitude1, longitude1),
        new google.maps.LatLng(latitude2, longitude2)
    ];

    // ラインを作成 
    var polyLineOptions = {
        path: mapPoints,
        strokeWeight: 5,
        strokeColor: getColor(semanticLinkID),
        strokeOpacity: "1.0"
    };

    // ラインを設定 
    var poly = new google.maps.Polyline(polyLineOptions);
    poly.id = semanticLinkID;
    poly.setMap(map);

    google.maps.event.addListener(poly, "click", function () {

        window.external.OnLineClicked(poly.id);
    });
}

function addCircle(latitude, longitude) {

    // 円を作成
    var circleOptions = {
        center: new google.maps.LatLng(latitude, longitude),
        radius: 3,
        strokeWeight: 3,
        strokeColor: "#000000",
        strokeOpacity: 1.0,
        fillColor: "#000000",
        fillOpacity: 1.0,
        zIndex: 2
    };

    // 円を設定
    var circle = new google.maps.Circle(circleOptions);
    circles.push(circle);
    circle.setMap(map);
}

function removeAllCircles() {

    for (var i = 0; i < circles.length; i++) {
        circles[i].setMap(null);
    }
}

function moveCurrentCircle(latitude, longitude) {

    if (currentCircle != null) {
        currentCircle.setMap(null);
    }

    var circleOptions = {
        center: new google.maps.LatLng(latitude, longitude),
        radius: 5,
        strokeWeight: 1,
        strokeColor: "#FF0000",
        strokeOpacity: 1.0,
        fillColor: "#FF0000",
        fillOpacity: 1.0,
        zIndex: 4
    }

    // 円を設定
    currentCircle = new google.maps.Circle(circleOptions);
    currentCircle.setMap(map);
}

function moveMap(latitude, longitude) {

    var location = new google.maps.LatLng(latitude, longitude);
    map.setCenter(location);
}

function addImageMarker(imagePath, latitude, longitude) {

    var imageMarker = new google.maps.Marker({
        position: new google.maps.LatLng(latitude, longitude),
        map: map,
        draggable: true,
        icon: imagePath
    });

    imageMarkers.push(imageMarker);
}

function removeAllImageMarker() {

    for (var i = 0; i < imageMarkers.length; i++) {
        imageMarkers[i].setMap(null);
    }
}