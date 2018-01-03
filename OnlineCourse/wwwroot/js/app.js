function commaSeparateNumber(val) {
    while (/(\d+)(\d{3})/.test(val.toString())) {
        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
    }
    return val;
}
function decimalRemove(val) {
    val = val.substring(0, val.indexOf("."));
    return val;
}
function getTotalCart() {
    var total = shoppingCart.totalCart();
    total = decimalRemove(total);
    if (total == 0) {
        $('.widget-body#details').hide();
    } else {
        $('.widget-body#details').show();
    }
    total = commaSeparateNumber(total);
    //console.log($("#total"));
    $("#total").text(total + " " + window.CurrencyCurrent);
}
function getCartList() {
    var list = shoppingCart.listCart();
   //console console.log(list);
    var cartBox = $("#cart_item");
    if (!list.length > 0) {
        cartBox.html("<p style='text-align:center;'>" + window.CartIsEmpty+"</p>");
    } else {
        var htmlstring = "";
        for (var i = 0; i < list.length; i++) {
            //console.log(list[i]);
            
            htmlstring+="<div class='title-row' id='item" + list[i].id + "'></div>" +
                "<div class='form-group row no-gutter'>" +
                "   <div class='col-xs-7 cart-food'>" +
                "       <span class='cart-food' style='padding-top: 8px;'> <span class='cart-food'>" + list[i].name + "</span>  <span style='padding:0 5px;'>" + list[i].price+"  </span>" +
                        "<span>" +
                "<i class='fa fa-times' aria-hidden='true'></i></span>" +
                "   </div>" +
                "   <div class='col-xs-3'>" +
                "        <input class='form-control count' min='" + list[i].min + "' max='" + list[i].max +"' type='number' value='" + list[i].count + "' id='input_" + list[i].id + "' data-id='" + list[i].id + "'>" +
                "   </div>" +
                "   <div class='col-xs-2'>" +
                "       <a class='btn_del_cart' href= '#' data-id='" + list[i].id + "' > <i class='fa fa-trash pull-left' style='padding:9px;'></i></a > " +
                "   </div>" +
                "   </div>";
        }
        cartBox.html(htmlstring);
    }
    

}
function navCartCount() {
    if (shoppingCart.countCart() > 0)
        $("#cart_badget").html(shoppingCart.countCart().toString());
    else
        $("#cart_badget").html("");
}
/** Modulo del Slider */
//var sliderModule = (function () {
//    var pb = {};
//    pb.el = $('#slider');
//    pb.items = {
//        panels: pb.el.find('.slider-wrapper > li'),
//    }
//    // Interval del Slider
//    var sliderInterval,
//        currentSlider = 0,
//        nextSlider = 1,
//        lengthSlider = pb.items.panels.length;
//    // Funcion para activar el Slider
//    var activateSlider = function () {
//        sliderInterval = setInterval(pb.startSlider, pb.settings.duration);
//    }
//    var changePanel = function (id) {
//        clearInterval(sliderInterval);
//        var items = pb.items,
//            controls = $('#control-buttons li');
//        // Comprobamos si el ID esta disponible entre los paneles
//        if (id >= lengthSlider) {
//            id = 0;
//        } else if (id < 0) {
//            id = lengthSlider - 1;
//        }
//        controls.removeClass('active').eq(id).addClass('active');
//        items.panels.eq(currentSlider).fadeOut('slow');
//        items.panels.eq(id).fadeIn('slow');
//        // Volvemos a actualizar los datos del slider
//        currentSlider = id;
//        nextSlider = id + 1;
//        // Reactivamos nuestro slider
//        activateSlider();
//    }
//    // Constructor del Slider
//    pb.init = function (settings) {
//        this.settings = settings || { duration: 8000 };
//        var items = this.items,
//            lengthPanels = items.panels.length,
//            output = '';
//        // Insertamos nuestros botones
//        for (var i = 0; i < lengthPanels; i++) {
//            if (i === 0) {
//                output += '<li class="active"></li>';
//            } else {
//                output += '<li></li>';
//            }
//        }
//        $('#control-buttons').html(output);
//        // Activamos nuestro Slider
//        activateSlider();
//        // Eventos para los controles
//        $('#control-buttons').on('click', 'li', function (e) {
//            var $this = $(this);
//            if (!(currentSlider === $this.index())) {
//                changePanel($this.index());
//            }
//        });
//    }
//    // Funcion para la Animacion
//    pb.startSlider = function () {
//        var items = pb.items,
//            controls = $('#control-buttons li');
//        // Comprobamos si es el ultimo panel para reiniciar el conteo
//        if (nextSlider >= lengthSlider) {
//            nextSlider = 0;
//            currentSlider = lengthSlider - 1;
//        }
//        controls.removeClass('active').eq(nextSlider).addClass('active');
//        items.panels.eq(currentSlider).fadeOut('slow');
//        items.panels.eq(nextSlider).fadeIn('slow');
//        // Actualizamos los datos del slider
//        currentSlider = nextSlider;
//        nextSlider += 1;
//    }
//    // Funcion para Cambiar de Panel con Los Controles
//    return pb;
//}());
function getUrlParameter(sParam) {
    var sPageUrl = decodeURIComponent(window.location.search.substring(1)), sUrlVariables = sPageUrl.split('&'), sParameterName, i;
    for (i = 0; i < sUrlVariables.length; i++) {
        sParameterName = sUrlVariables[i].split('=');
        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};


function getDayOfWeek(daysindexes) {

    var days = daysindexes.split(",");
    var daysName = "";
    for (var i = 0; i < days.length; i++) {
        if (days[i] == '0') {
            daysName += "شنبه ";
        } if (days[i] == '1') {
            daysName += "یکشنبه ";
        } if (days[i] == '2') {
            daysName += "دوشنبه ";
        } if (days[i] == '3') {
            daysName += "سه شنبه ";
        } if (days[i] == '4') {
            daysName += "چهارشنبه ";
        } if (days[i] == '5') {
            daysName += "پنجشنبه ";
        } if (days[i] == '6') {
            daysName += "جمعه ";
        }

    }
    return daysName;
}