﻿// StyleFix 1.0.2 + PrefixFree 1.0.6 / Lea Verou / MIT license
(function () {
    function h(a, b) {
        return [].slice.call((b || document).querySelectorAll(a))
    }
    if (window.addEventListener) {
        var e = window.StyleFix = {
            link: function (a) {
                try {
                    if ("stylesheet" !== a.rel || a.hasAttribute("data-noprefix")) return
                } catch (b) {
                    return
                }
                var c = a.href || a.getAttribute("data-href"),
                    f = c.replace(/[^\/]+$/, ""),
                    i = a.parentNode,
                    d = new XMLHttpRequest,
                    g;
                d.onreadystatechange = function () {
                    4 === d.readyState && g()
                };
                g = function () {
                    var b = d.responseText;
                    if (b && a.parentNode && (!d.status || 400 > d.status || 600 < d.status)) {
                        b = e.fix(b, !0, a);
                        f && (b = b.replace(/url\(\s*?((?:"|')?)(.+?)\1\s*?\)/gi, function (b, a, c) {
                            return !/^([a-z]{3,10}:|\/|#)/i.test(c) ? 'url("' + f + c + '")' : b
                        }), b = b.replace(RegExp("\\b(behavior:\\s*?url\\('?\"?)" + f, "gi"), "$1"));
                        var c = document.createElement("style");
                        c.textContent = b;
                        c.media = a.media;
                        c.disabled = a.disabled;
                        c.setAttribute("data-href", a.getAttribute("href"));
                        i.insertBefore(c, a);
                        i.removeChild(a)
                    }
                };
                try {
                    d.open("GET", c), d.send(null)
                } catch (k) {
                    "undefined" != typeof XDomainRequest && (d = new XDomainRequest, d.onerror = d.onprogress =
                        function () { }, d.onload = g, d.open("GET", c), d.send(null))
                }
                a.setAttribute("data-inprogress", "")
            },
            styleElement: function (a) {
                var b = a.disabled;
                a.textContent = e.fix(a.textContent, !0, a);
                a.disabled = b
            },
            styleAttribute: function (a) {
                var b = a.getAttribute("style"),
                    b = e.fix(b, !1, a);
                a.setAttribute("style", b)
            },
            process: function () {
                h('link[rel="stylesheet"]:not([data-inprogress])').forEach(StyleFix.link);
                h("style").forEach(StyleFix.styleElement);
                h("[style]").forEach(StyleFix.styleAttribute)
            },
            register: function (a, b) {
                (e.fixers =
                    e.fixers || []).splice(void 0 === b ? e.fixers.length : b, 0, a)
            },
            fix: function (a, b) {
                for (var c = 0; c < e.fixers.length; c++) a = e.fixers[c](a, b) || a;
                return a
            },
            camelCase: function (a) {
                return a.replace(/-([a-z])/g, function (b, a) {
                    return a.toUpperCase()
                }).replace("-", "")
            },
            deCamelCase: function (a) {
                return a.replace(/[A-Z]/g, function (a) {
                    return "-" + a.toLowerCase()
                })
            }
        };
        (function () {
            setTimeout(function () {
                h('link[rel="stylesheet"]').forEach(StyleFix.link)
            }, 10);
            document.addEventListener("DOMContentLoaded", StyleFix.process, !1)
        })()
    }
})();
(function (h) {
    function e(b, c, f, i, d) {
        b = a[b];
        b.length && (b = RegExp(c + "(" + b.join("|") + ")" + f, "gi"), d = d.replace(b, i));
        return d
    }
    if (window.StyleFix && window.getComputedStyle) {
        var a = window.PrefixFree = {
            prefixCSS: function (b, c) {
                var f = a.prefix,
                    b = e("functions", "(\\s|:|,)", "\\s*\\(", "$1" + f + "$2(", b),
                    b = e("keywords", "(\\s|:)", "(\\s|;|\\}|$)", "$1" + f + "$2$3", b),
                    b = e("properties", "(^|\\{|\\s|;)", "\\s*:", "$1" + f + "$2:", b);
                if (a.properties.length) var i = RegExp("\\b(" + a.properties.join("|") + ")(?!:)", "gi"),
                    b = e("valueProperties", "\\b",
                        ":(.+?);",
                        function (a) {
                            return a.replace(i, f + "$1")
                        }, b);
                c && (b = e("selectors", "", "\\b", a.prefixSelector, b), b = e("atrules", "@", "\\b", "@" + f + "$1", b));
                return b = b.replace(RegExp("-" + f, "g"), "-")
            },
            property: function (b) {
                return (a.properties.indexOf(b) ? a.prefix : "") + b
            },
            value: function (b) {
                b = e("functions", "(^|\\s|,)", "\\s*\\(", "$1" + a.prefix + "$2(", b);
                return b = e("keywords", "(^|\\s)", "(\\s|$)", "$1" + a.prefix + "$2$3", b)
            },
            prefixSelector: function (b) {
                return b.replace(/^:{1,2}/, function (b) {
                    return b + a.prefix
                })
            },
            prefixProperty: function (b,
                c) {
                var f = a.prefix + b;
                return c ? StyleFix.camelCase(f) : f
            }
        };
        (function () {
            var b = {},
                c = [],
                f = getComputedStyle(document.documentElement, null),
                i = document.createElement("div").style,
                d = function (a) {
                    if ("-" === a.charAt(0)) {
                        c.push(a);
                        var a = a.split("-"),
                            d = a[1];
                        for (b[d] = ++b[d] || 1; 3 < a.length;) a.pop(), d = a.join("-"), StyleFix.camelCase(d) in i && -1 === c.indexOf(d) && c.push(d)
                    }
                };
            if (0 < f.length)
                for (var g = 0; g < f.length; g++) d(f[g]);
            else
                for (var e in f) d(StyleFix.deCamelCase(e));
            var g = 0,
                j, h;
            for (h in b) f = b[h], g < f && (j = h, g = f);
            a.prefix =
                "-" + j + "-";
            a.Prefix = StyleFix.camelCase(a.prefix);
            a.properties = [];
            for (g = 0; g < c.length; g++) e = c[g], 0 === e.indexOf(a.prefix) && (j = e.slice(a.prefix.length), StyleFix.camelCase(j) in i || a.properties.push(j));
            "Ms" == a.Prefix && !("transform" in i) && !("MsTransform" in i) && "msTransform" in i && a.properties.push("transform", "transform-origin");
            a.properties.sort()
        })();
        (function () {
            function b(a, b) {
                e[b] = "";
                e[b] = a;
                return !!e[b]
            }
            var c = {
                "linear-gradient": {
                    property: "backgroundImage",
                    params: "red, teal"
                },
                calc: {
                    property: "width",
                    params: "1px + 5%"
                },
                element: {
                    property: "backgroundImage",
                    params: "#foo"
                },
                "cross-fade": {
                    property: "backgroundImage",
                    params: "url(a.png), url(b.png), 50%"
                }
            };
            c["repeating-linear-gradient"] = c["repeating-radial-gradient"] = c["radial-gradient"] = c["linear-gradient"];
            var f = {
                initial: "color",
                "zoom-in": "cursor",
                "zoom-out": "cursor",
                box: "display",
                flexbox: "display",
                "inline-flexbox": "display"
            };
            a.functions = [];
            a.keywords = [];
            var e = document.createElement("div").style,
                d;
            for (d in c) {
                var g = c[d],
                    h = g.property,
                    g = d + "(" + g.params + ")";
                !b(g, h) && b(a.prefix +
                    g, h) && a.functions.push(d)
            }
            for (var j in f) h = f[j], !b(j, h) && b(a.prefix + j, h) && a.keywords.push(j)
        })();
        (function () {
            function b(a) {
                e.textContent = a + "{}";
                return !!e.sheet.cssRules.length
            }
            var c = {
                    ":read-only": null,
                    ":read-write": null,
                    ":any-link": null,
                    "::selection": null
                },
                f = {
                    keyframes: "name",
                    viewport: null,
                    document: 'regexp(".")'
                };
            a.selectors = [];
            a.atrules = [];
            var e = h.appendChild(document.createElement("style")),
                d;
            for (d in c) {
                var g = d + (c[d] ? "(" + c[d] + ")" : "");
                !b(g) && b(a.prefixSelector(g)) && a.selectors.push(d)
            }
            for (var k in f) g =
                k + " " + (f[k] || ""), !b("@" + g) && b("@" + a.prefix + g) && a.atrules.push(k);
            h.removeChild(e)
        })();
        a.valueProperties = ["transition", "transition-property"];
        h.className += " " + a.prefix;
        StyleFix.register(a.prefixCSS)
    }
})(document.documentElement);