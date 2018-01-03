// ***************************************************
// Shopping Cart functions

var shoppingCart = (function () {
    // Private methods and properties
    var cart = [];

    function Item(id, name, price, count, mama, min, max, deliverDateTime) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.mama = mama;
        this.min = min;
        this.max = max;
        this.deliverDateTime = deliverDateTime;
        this.count = parseInt(count);
    }

    function saveCart() {
        setCookie("shoppingCart", JSON.stringify(cart));
        //localStorage.setItem("shoppingCart", JSON.stringify(cart));
    }

    function setCookie(key, value) {
        var expires = new Date();
        expires.setTime(expires.getTime() + (1 * 24 * 60 * 60 * 1000));
        document.cookie = key + '=' + Base64.encode(value) + ';expires=' + expires.toUTCString()+';path=/';
    }

    function getCookie(key) {
        //var keyValue = Base64.decode(document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)'));
        var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
        return keyValue ? Base64.decode(keyValue[2]) : null;
    }

    function loadCart() {
        //cart = JSON.parse(localStorage.getItem("shoppingCart"));
        var decode = getCookie("shoppingCart");
        if (decode !== null && decode !== 'e') {
            cart = JSON.parse( decode);
        }
        
        if (cart === null) {
            cart = [];
        }
        console.log(cart);
    }

    //function utf8_to_b64(str) {
    //    return window.btoa(unescape(encodeURIComponent(str)));
    //}

    //function b64_to_utf8(str) {
    //    return decodeURIComponent(escape(window.atob(str)));
    //}

    function indexOf(id) {
        for (var i in cart) {
            if (cart[i].id === id) {
                return i;
            }
        }
        return -1;
    }

    loadCart();



    // Public methods and properties
    var obj = {};

    obj.addItemToCart = function (id, name, price, count, mama, min, max, deliverDateTime) {
        console.log(indexOf(id));
        var i = indexOf(id);
        if (i===-1) {
            count = min;
            var item = new Item(id, name, price, count, mama, min, max, deliverDateTime);
            cart.push(item);  
            saveCart();
        } else {         
            var cnt = parseInt(cart[i].count);
            cnt += parseInt(count);
            if (cnt <= max) {
                cart[i].count = cnt;
                saveCart();
            } else {

                alert("اتمام موجودی");
            }
        }
    };

    obj.setCountForItem = function (id, count) {
        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count = count;
                break;
            }
        }
        saveCart();
    };


    obj.removeItemFromCart = function (id) { // Removes one item
        for (var i in cart) {
            if (cart[i].id === id) { // "3" === 3 false
                cart[i].count--; // cart[i].count --
                if (cart[i].count === 0) {
                    cart.splice(i, 1);
                }
                break;
            }
        }
        saveCart();
    };


    obj.removeItemFromCartAll = function (id) { // removes all item name
        for (var i in cart) {
            if (cart[i].id === id) {
                cart.splice(i, 1);
                break;
            }
        }
        saveCart();
    };


    obj.clearCart = function () {
        cart = [];
        saveCart();
    }


    obj.countCart = function () { // -> return total count
        var totalCount = 0;
        for (var i in cart) {
            totalCount += parseInt(cart[i].count);
        }

        return totalCount;
    };

    obj.totalCart = function () { // -> return total cost
        var totalCost = 0;
        for (var i in cart) {
            if (cart.hasOwnProperty(i)) {
                totalCost += cart[i].price * cart[i].count;
            }
        }
        return totalCost.toFixed(2);
    };

    obj.listCart = function () { // -> array of Items
        var cartCopy = [];
        //console.log("Listing cart");
        //console.log(cart);
        for (var i in cart) {
            //console.log(i);
            var item = cart[i];
            var itemCopy = {};
            for (var p in item) {
                itemCopy[p] = item[p];
            }
            itemCopy.total = (item.price * item.count).toFixed(2);
            cartCopy.push(itemCopy);
        }
        return cartCopy;
    };

    // ----------------------------
    return obj;
})();




