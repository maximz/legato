(function (f)
{
	f.fn.raty = function (r)
	{
		if (this.length == 0)
		{
			a("Selector invalid or missing!");
			return;
		}
		else
		{
			if (this.length > 1)
			{
				return this.each(function ()
				{
					f.fn.raty.apply(f(this), [r]);
				});
			}
		}
		var o = f.extend(
		{}, f.fn.raty.defaults, r),
			v = f(this),
			m = this.attr("id"),
			n = 0,
			w = o.starOn,
			s = "",
			u = o.target,
			l = (o.width) ? o.width : (o.number * o.size + o.number * 4);
		if (m === undefined)
		{
			m = "raty-" + v.index();
			v.attr("id", m);
		}
		if (o.number > 20)
		{
			o.number = 20;
		}
		else
		{
			if (o.number < 0)
			{
				o.number = 0;
			}
		}
		if (o.path.substring(o.path.length - 1, o.path.length) != "/")
		{
			o.path += "/";
		}
		v.data("options", o);
		if (!isNaN(parseInt(o.start)) && o.start > 0)
		{
			n = (o.start > o.number) ? o.number : o.start;
		}
		for (var t = 1; t <= o.number; t++)
		{
			w = (n >= t) ? o.starOn : o.starOff;
			s = (t <= o.hintList.length && o.hintList[t - 1] !== null) ? o.hintList[t - 1] : t;
			v.append('<img id="' + m + "-" + t + '" src="' + o.path + w + '?' + o.revnum + '" alt="' + t + '" title="' + s + '" class="' + m + '"/>').append((t < o.number) ? "&nbsp;" : "");
		}
		if (o.iconRange && n > 0)
		{
			b(m, n, o);
		}
		var q = f("<input/>", {
			id: m + "-score",
			type: "hidden",
			name: o.scoreName
		}).appendTo(v);
		if (n > 0)
		{
			q.val(n);
		}
		if (o.half)
		{
			c(v, f("input#" + m + "-score").val(), o);
		}
		if (!o.readOnly)
		{
			if (u !== null)
			{
				u = f(u);
				if (u.length == 0)
				{
					a("Target selector invalid or missing!");
				}
			}
			if (o.cancel)
			{
				var p = f("img." + m),
					x = '<img src="' + o.path + o.cancelOff + '?' + o.revnum + '" alt="x" title="' + o.cancelHint + '" class="button-cancel"/>';
				if (o.cancelPlace == "left")
				{
					v.prepend(x + "&nbsp;");
				}
				else
				{
					v.append("&nbsp;").append(x);
				}
				f("#" + m + " img.button-cancel").mouseenter(function ()
				{
					f(this).attr("src", o.path + o.cancelOn);
					p.attr("src", o.path + o.starOff);
					d(u, "", o);
				}).mouseleave(function ()
				{
					f(this).attr("src", o.path + o.cancelOff);
					v.mouseout();
				}).click(function (y)
				{
					f("input#" + m + "-score").removeAttr("value");
					if (o.click)
					{
						o.click.apply(v, [null, y]);
					}
				});
				v.css("width", l + o.size + 4);
			}
			else
			{
				v.css("width", l);
			}
			v.css("cursor", "pointer");
			h(v, o, u);
		}
		else
		{
			v.css("cursor", "default");
			j(v, n, o);
		}
		return v;
	};

	function h(n, m, o)
	{
		var q = n.attr("id"),
			p = f("input#" + q + "-score"),
			l = n.children("img." + q);
		n.mouseleave(function ()
		{
			e(n, p.val(), m);
			g(o, p, m);
		});
		l.bind(((m.half) ? "mousemove" : "mouseover"), function (s)
		{
			b(q, this.alt, m);
			if (m.half)
			{
				var r = parseFloat(((s.pageX - f(this).offset().left) / m.size).toFixed(1));
				r = (r >= 0 && r < 0.5) ? 0.5 : 1;
				n.data("score", parseFloat(this.alt) + r - 1);
				c(n, n.data("score"), m);
			}
			else
			{
				b(q, this.alt, m);
			}
			d(o, this.alt, m);
		}).click(function (r)
		{
			p.val(m.half ? n.data("score") : this.alt);
			if (m.click)
			{
				m.click.apply(n, [p.val(), r]);
			}
		});
	}
	function g(n, o, l)
	{
		if (n !== null)
		{
			var m = "";
			if (l.targetKeep)
			{
				m = o.val();
				if (l.targetType == "hint")
				{
					if (o.val() == "" && l.cancel)
					{
						m = l.cancelHint;
					}
					else
					{
						m = l.hintList[Math.ceil(o.val()) - 1];
					}
				}
			}
			if (i(n))
			{
				n.val(m);
			}
			else
			{
				n.html(m);
			}
		}
	}
	function k(p, m, l)
	{
		var n = undefined;
		if (m == undefined)
		{
			a("Specify an ID or class to be the target of the action.");
			return;
		}
		if (m)
		{
			if (m.indexOf(".") >= 0)
			{
				var o;
				return f(m).each(function ()
				{
					o = "#" + f(this).attr("id");
					if (l == "start")
					{
						f.fn.raty.start(p, o);
					}
					else
					{
						if (l == "click")
						{
							f.fn.raty.click(p, o);
						}
						else
						{
							if (l == "readOnly")
							{
								f.fn.raty.readOnly(p, o);
							}
						}
					}
				});
			}
			n = f(m);
			if (!n.length)
			{
				a('"' + m + '" is a invalid identifier for the public funtion $.fn.raty.' + l + "().");
				return;
			}
		}
		return n;
	}
	function a(l)
	{
		if (window.console && window.console.log)
		{
			window.console.log(l);
		}
	}
	function b(l, n, m)
	{
		var o = f("img." + l).length,
			t = 0,
			r = 0,
			s, p;
		for (var q = 1; q <= o; q++)
		{
			s = f("img#" + l + "-" + q);
			if (q <= n)
			{
				if (m.iconRange && m.iconRange.length > t)
				{
					p = m.iconRange[t][0];
					r = m.iconRange[t][1];
					if (q <= r)
					{
						s.attr("src", m.path + p);
					}
					if (q == r)
					{
						t++;
					}
				}
				else
				{
					s.attr("src", m.path + m.starOn);
				}
			}
			else
			{
				s.attr("src", m.path + m.starOff);
			}
		}
	}
	function j(m, n, l)
	{
		if (n != 0)
		{
			n = parseInt(n);
			hint = (n > 0 && l.number <= l.hintList.length && l.hintList[n - 1] !== null) ? l.hintList[n - 1] : n;
		}
		else
		{
			hint = l.noRatedMsg;
		}
		m.attr("title", hint).children("img").attr("title", hint);
	}
	function i(l)
	{
		return l.is("input") || l.is("select") || l.is("textarea");
	}
	function e(m, n, l)
	{
		var o = m.attr("id");
		if (isNaN(parseInt(n)))
		{
			m.children("img." + o).attr("src", l.path + l.starOff);
			f("input#" + o + "-score").removeAttr("value");
			return;
		}
		if (n < 0)
		{
			n = 0;
		}
		else
		{
			if (n > l.number)
			{
				n = l.number;
			}
		}
		b(o, n, l);
		if (n > 0)
		{
			f("input#" + o + "-score").val(n);
			if (l.half)
			{
				c(m, n, l);
			}
		}
		if (l.readOnly || m.css("cursor") == "default")
		{
			j(m, n, l);
		}
	}
	function d(o, n, l)
	{
		if (o !== null)
		{
			var m = n;
			if (l.targetType == "hint")
			{
				if (n == 0 && l.cancel)
				{
					m = l.cancelHint;
				}
				else
				{
					m = l.hintList[n - 1];
				}
			}
			if (i(o))
			{
				o.val(m);
			}
			else
			{
				o.html(m);
			}
		}
	}
	function c(n, p, m)
	{
		var q = n.attr("id"),
			l = Math.ceil(p),
			o = (l - p).toFixed(1);
		if (o > 0.25 && o <= 0.75)
		{
			l = l - 0.5;
			f("img#" + q + "-" + Math.ceil(l)).attr("src", m.path + m.starHalf);
		}
		else
		{
			if (o > 0.75)
			{
				l--;
			}
			else
			{
				f("img#" + q + "-" + l).attr("src", m.path + m.starOn);
			}
		}
	}
	f.fn.raty.cancel = function (l, n)
	{
		var m = (n === undefined) ? false : true;
		if (m)
		{
			f.fn.raty.click("", l, "cancel");
		}
		else
		{
			f.fn.raty.start("", l, "cancel");
		}
		return f.fn.raty;
	};
	f.fn.raty.click = function (o, m)
	{
		var n = k(o, m, "click"),
			l = f(m).data("options");
		e(n, o, l);
		if (l.click)
		{
			l.click.apply(n, [o]);
		}
		else
		{
			a('You must add the "click: function(score, evt) { }" callback.');
		}
		return f.fn.raty;
	};
	f.fn.raty.readOnly = function (o, m)
	{
		var n = k(o, m, "readOnly"),
			l = f(m).data("options"),
			p = n.children("img.button-cancel");
		if (p[0])
		{
			(o) ? p.hide() : p.show();
		}
		if (o)
		{
			f("img." + n.attr("id")).unbind();
			n.css("cursor", "default").unbind();
		}
		else
		{
			h(n, l);
			n.css("cursor", "pointer");
		}
		return f.fn.raty;
	};
	f.fn.raty.start = function (o, m)
	{
		var n = k(o, m, "start"),
			l = f(m).data("options");
		e(n, o, l);
		return f.fn.raty;
	};
	f.fn.raty.defaults = {
		cancel: false,
		cancelHint: "cancel this rating!",
		cancelOff: "cancel-off.png",
		cancelOn: "cancel-on.png",
		cancelPlace: "left",
		click: null,
		half: false,
		hintList: ["bad", "poor", "regular", "good", "gorgeous"],
		noRatedMsg: "not rated yet",
		number: 5,
		path: "img/",
		iconRange: [],
		readOnly: false,
		revnum: '1',
		scoreName: "score",
		size: 16,
		starHalf: "star-half.png",
		starOff: "star-off.png",
		starOn: "star-on.png",
		start: 0,
		target: null,
		targetKeep: false,
		targetType: "hint",
		width: null
	};
})(jQuery);