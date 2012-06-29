/*
 *  TypeWatch 2.0 - Original by Denny Ferrassoli / Refactored by Charles Christolini
 *
 *  Examples/Docs: www.dennydotnet.com
 *  
 *  Copyright(c) 2007 Denny Ferrassoli - DennyDotNet.com
 *  Coprright(c) 2008 Charles Christolini - BinaryPie.com
 *  
 *  Dual licensed under the MIT and GPL licenses:
 *  http://www.opensource.org/licenses/mit-license.php
 *  http://www.gnu.org/licenses/gpl.html
*/

(function(a) {
    a.fn.typeWatch = function(c) {
        var f = a.extend({
            wait: 750,
            callback: function() {},
            highlight: true,
            captureLength: 2
        }, c);
        function d(g, h) {
            var i = a(g.el).val();
            if (i.length > f.captureLength && i.toUpperCase() != g.text || h && i.length > f.captureLength) {
                g.text = i.toUpperCase();
                g.cb(i);
            }
        }
        function e(i) {
            if (i.type.toUpperCase() == "TEXT" || i.nodeName.toUpperCase() == "TEXTAREA") {
                var g = {
                    timer: null,
                    text: a(i).val().toUpperCase(),
                    cb: f.callback,
                    el: i,
                    wait: f.wait
                };
                if (f.highlight) {
                    a(i).focus(function() {
                        this.select();
                    });
                }
                var h = function(n) {
                    var k = g.wait;
                    var l = false;
                    if (n.keyCode == 13 && this.type.toUpperCase() == "TEXT") {
                        k = 1;
                        l = true;
                    }
                    var j = function() {
                        d(g, l);
                    };
                    clearTimeout(g.timer);
                    g.timer = setTimeout(j, k);
                };
                a(i).keydown(h);
            }
        }
        return this.each(function(g) {
            e(this);
        });
    };
})(jQuery);