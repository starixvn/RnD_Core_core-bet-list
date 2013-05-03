if (typeof (Fanex) == 'undefined') { // Require Fanex namespace
	Fanex = {};
}
if (typeof (Fanex.PTEngine) == 'undefined') { // Require Fanex.PTEngine namespace
	alert('Require Fanex.PTEngine namespace');
}
// Function name format Render_[RoleId_]TargetRoleId_Form
// [RoleId_] is optional
// Call method by Method.call(this, arg1, arg2...)
Fanex.PTEngine.BetType.prototype.Render_2_1_1 = function (owner) { // a1
	if (typeof (this.pt.a1) == 'undefined') {
		this.pt.l2 = this.pt.a2;
		this.pt.a1 = this.pt.a2;
	}
	else {
		this.pt.l2 = this.pt.a2;
	}
	box = document.createElement("DIV");

	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';

	// a1 selection
	var min = this.pt.n2;
	var max = Math.min(this.pt.l2, this.pt.a2);
	var selected = this.pt.a1;
	var a1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	a1Select.select.id = a1Select.select.name = "a1$" + this.suffixId;
	a1Select.className = "BetTypeSelect";
	a1Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(a1Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";

	// Get updated data
	this.Save = function () {
		this.pt.a1 = parseFloat(a1Select.select.value);
		return {
			id: this.id,
			pt: this.pt
		}
	}
	return box;
};
Fanex.PTEngine.BetType.prototype.Render_3_1_1 = function (owner) { // m1, um1, im1
    box = document.createElement("DIV");
    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BetTypeHeader";
    box.appendChild(header); // Header bet type name
    header.style.minWidth = this.minWidth + 'px';
    // a1 label
    var a1Label = document.createElement("DIV");
    a1Label.innerHTML = this.pt.a1;
    a1Label.className = "BetTypeLabel";
    a1Label.style.minWidth = this.minWidth + 'px';
    box.appendChild(a1Label);

    // m1 selection
    if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
        if (!this.pt.im2) {
            var min = this.pt.n3;
            var max = this.pt.m3;
            var selected = this.pt.m1;
            var m1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
            m1Select.select.id = m1Select.select.name = "m1$" + this.suffixId;
            m1Select.className = "BetTypeSelect";
            m1Select.style.minWidth = this.minWidth + 'px';
            box.appendChild(m1Select);
        } else {
            // m1 label
            var m1Label = document.createElement("DIV");
            m1Label.innerHTML = this.pt.m1;
            m1Label.className = "BetTypeLabel";
            m1Label.style.minWidth = this.minWidth + 'px';
            box.appendChild(m1Label);
        }

        var im1Check = null;
        var um1Select = null;
        // im1
        im1Check = document.createElement("INPUT");
        im1Check.checked = im1Check.defaultChecked = this.pt.im1;
        im1Check.name = "im1$" + this.suffixId;
        im1Check.type = "checkbox";
        var im1Div = document.createElement("DIV");
        im1Div.className = "BetTypeSelect";
        im1Div.style.minWidth = this.minWidth + 'px';
        im1Div.appendChild(im1Check);
        box.appendChild(im1Div);
        // um1
        min = 0;
        max = this.pt.um2;
        selected = this.pt.um1;
        um1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
        um1Select.select.id = um1Select.select.name = "um1$" + this.suffixId;
        um1Select.className = "BetTypeSelect";
        um1Select.style.minWidth = this.minWidth + 'px';
        box.appendChild(um1Select);
        // im1 handling
        var im2 = this.pt.im2;
        um1Select.select.disabled = !this.pt.im1;
        if (!im2) {
            m1Select.select.disabled = im1Check.checked;
        }

        im1Check.onclick = function () {
            um1Select.select.disabled = !im1Check.checked;
            if (!im2) {
                m1Select.select.disabled = im1Check.checked;
            }
        }
    }
    else {
        var min = this.pt.n3;
        var max = this.pt.m3;
        var selected = this.pt.m1;
        var m1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
        m1Select.select.id = m1Select.select.name = "m1$" + this.suffixId;
        m1Select.className = "BetTypeSelect";
        m1Select.style.minWidth = this.minWidth + 'px';
        box.appendChild(m1Select);
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    // Get updated data
    this.Save = function () {
        if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
            this.pt.im1 = im1Check.checked;

            if (!this.pt.im1) {
                if (!this.pt.im2) {
                    this.pt.m1 = parseFloat(m1Select.select.value);
                }

                this.pt.um1 = 0;
            }
            else {
                this.pt.m1 = 0;
                this.pt.um1 = parseFloat(um1Select.select.value);
            }
        } else {
            this.pt.m1 = parseFloat(m1Select.select.value);
        }

        return {
            id: this.id,
            pt: this.pt
        }
    }
    return box;
};
Fanex.PTEngine.BetType.prototype.Render_4_1_1 = function (owner) { // s1, us1, is1
    box = document.createElement("DIV");

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BetTypeHeader";
    box.appendChild(header); // Header bet type name
    header.style.minWidth = this.minWidth + 'px';

    // a1 label
    var a1Label = document.createElement("DIV");
    a1Label.innerHTML = this.pt.a1;
    a1Label.className = "BetTypeLabel";
    a1Label.style.minWidth = this.minWidth + 'px';
    box.appendChild(a1Label);

    // m1 label
    var m1Label = document.createElement("DIV");
    m1Label.innerHTML = this.pt.m1;
    m1Label.className = "BetTypeLabel";
    m1Label.style.minWidth = this.minWidth + 'px';
    box.appendChild(m1Label);

    // s1
    var s1Select = null;
    if (!this.pt.is3) {
        var min = 0;
        var max = this.pt.a2;
        var selected = this.pt.s1;
        s1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
        s1Select.select.id = s1Select.select.name = "s1$" + this.suffixId;
        s1Select.select.disabled = this.pt.is1;
        s1Select.className = "BetTypeSelect";
        s1Select.style.minWidth = this.minWidth + 'px';
        box.appendChild(s1Select);
    } else {
        // s1 label
        var s1Label = document.createElement("DIV");
        s1Label.innerHTML = this.pt.s1;
        s1Label.className = "BetTypeLabel";
        s1Label.style.minWidth = this.minWidth + 'px';
        box.appendChild(s1Label);
    }

    // is1
    var is1Check = document.createElement("INPUT");
    is1Check.checked = is1Check.defaultChecked = this.pt.is1;
    is1Check.name = "is1$" + this.suffixId;
    is1Check.title = "Click here to check Auto Super on Member";
    is1Check.type = "checkbox";
    var is1Div = document.createElement("DIV");
    is1Div.className = "BetTypeSelect";
    is1Div.style.minWidth = this.minWidth + 'px';
    is1Div.appendChild(is1Check);
    box.appendChild(is1Div);

    // us1
    min = 0;
    max = this.pt.s4;
    selected = this.pt.us1;
    var us1Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    us1Select.select.id = us1Select.select.name = "us1$" + this.suffixId;
    us1Select.select.disabled = !this.pt.is1;
    us1Select.className = "BetTypeSelect";
    us1Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(us1Select);
    // is1 handling
    var is3 = this.pt.is3;
    is1Check.onclick = function () {
        us1Select.select.disabled = !is1Check.checked;
        if (!is3) {
            s1Select.select.disabled = is1Check.checked;
        }
    }
    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    // Get updated data
    this.Save = function () {  
        this.pt.is1 = is1Check.checked;
        if (!this.pt.is1) {
            if (!this.pt.is3) {
                this.pt.s1 = parseFloat(s1Select.select.value);
            }

            this.pt.us1 = 0;
        }
        else {
            this.pt.us1 = parseFloat(us1Select.select.value);
            this.pt.s1 = 0;
        }

        return {
            id: this.id,
            pt: this.pt
        }
    }

    return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_2_1 = function (owner) { // m2, a2, im2, um2
    if (typeof (this.pt.a2) == 'undefined') { // Add new case
        this.pt.a2 = 0;
        this.pt.um2 = 0;
        this.pt.l3 = this.pt.m3;
        this.pt.n2 = 0;
    }

    var n3 = this.pt.n3;
    var m3 = this.pt.m3;
    var a4 = this.pt.a4;
    var l3 = this.pt.l3;
    var n2 = this.pt.n2;
    var im2 = this.pt.im2;
    var um2 = im2 ? this.pt.um2 : 0;

    box = document.createElement("DIV");
    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BetTypeHeader";
    box.appendChild(header); // Header bet type name
    header.style.minWidth = this.minWidth + 'px';

    // a2
    var min = Math.max(n2, im2 ? 0 : Fanex.Round(n3 - m3));
    var max = im2 ? Math.min(l3, a4) : Math.min(Fanex.Round(l3 - m3), a4);
    var selected = this.pt.a2;
    var a2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    a2Select.select.id = a2Select.select.name = "a2$" + this.suffixId;
    a2Select.className = "BetTypeSelect";
    a2Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(a2Select);

    // m2
    var min = Math.max(0, Fanex.Round(n3 - a4));
    var max = Fanex.Round(l3 - n2);
    var selected = m3;
    var m2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    m2Select.select.id = m2Select.select.name = "m2$" + this.suffixId;
    m2Select.select.disabled = this.pt.im2;
    m2Select.className = "BetTypeSelect";
    m2Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(m2Select);

    var im2Check = null;
    var um2Select = null;

    if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
        // im2
        im2Check = document.createElement("INPUT");
        im2Check.checked = im2Check.defaultChecked = this.pt.im2;
        im2Check.name = "im2$" + this.suffixId;
        im2Check.type = "checkbox";
        var im2Div = document.createElement("DIV");
        im2Div.className = "BetTypeSelect";
        im2Div.style.minWidth = this.minWidth + 'px';
        im2Div.appendChild(im2Check);
        box.appendChild(im2Div);

        // um2
        var min = 0;
        var max = l3;
        selected = um2;
        um2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
        um2Select.select.id = um2Select.select.name = "um2$" + this.suffixId;
        um2Select.select.disabled = !this.pt.im2;
        um2Select.className = "BetTypeSelect";
        um2Select.style.minWidth = this.minWidth + 'px';
        box.appendChild(um2Select);

        // Event change m2, reset a2Select
        m2Select.select.onchange = function (e, keepCurrent) {
            var min = Fanex.Round(im2Check.checked ? n2 : n3 - parseFloat(this.value));
            min = Math.max(min, n2);
            var max = im2Check.checked ? Math.min(l3, a4) : Math.min(Fanex.Round(l3 - parseFloat(this.value)), a4);
            var selected = parseFloat(a2Select.select.value);
            if (!keepCurrent || selected < min || selected > max) {
                selected = max;
            }
            Fanex.PTEngine.PT_SetSelect(a2Select.select, min, max, Fanex.PTEngine.PT_STEP, selected);
        }

        // im2 handling
        im2Check.onclick = function (e) {
            // Detect current value is out of range, reset it to max
            Fanex.PTEngine.PT_ValidateSelect(this.checked ? um2Select.select : m2Select.select);

            m2Select.select.onchange(e, true);
            um2Select.select.disabled = !im2Check.checked;
            m2Select.select.disabled = im2Check.checked;
        }
    }
    else {
        // Event change m2, reset a2Select
        m2Select.select.onchange = function () {
            var min = Fanex.Round(n3 - parseFloat(this.value));
            min = Math.max(min, n2);
            var max = Math.min(Fanex.Round(l3 - parseFloat(this.value)), a4);
            var selected = max;
            Fanex.PTEngine.PT_SetSelect(a2Select.select, min, max, Fanex.PTEngine.PT_STEP, selected);
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    // Get updated data
    this.Save = function () {
        if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
            this.pt.a2 = parseFloat(a2Select.select.value);
            this.pt.im2 = im2Check.checked;

            if (this.pt.im2) {
                this.pt.m2 = 0;
                this.pt.um2 = parseFloat(um2Select.select.value);
            }
            else {
                this.pt.m2 = parseFloat(m2Select.select.value);
                this.pt.um2 = 0;
            }
        }
        else {
            this.pt.m2 = parseFloat(m2Select.select.value);
            this.pt.a2 = parseFloat(a2Select.select.value);
        }
        return {
            id: this.id,
            pt: this.pt
        }
    }
    return box;
};
Fanex.PTEngine.BetType.prototype.Render_3_2_2 = function (owner) { // n2
	box = document.createElement("DIV");
	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';
	// n2
	var min = 0;
	var max = Math.min(this.pt.l2, this.pt.a2);
	var selected = this.pt.n2;
	var n2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	n2Select.select.id = n2Select.select.name = "n2$" + this.suffixId;
	n2Select.className = "BetTypeSelect";
	n2Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(n2Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";
	// Get updated data
	this.Save = function () {
	    this.pt.n2 = parseFloat(n2Select.select.value);
	    this.pt.l2 = 0;

		return {
			id: this.id,
			pt: this.pt
		}
    }

	return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_3_1 = function (owner) { // s3, m3, is3, us3, min/max: m4, n3, s4
    box = document.createElement("DIV");
    if (typeof (this.pt.m3) == 'undefined') {
        this.pt.n3 = 0;
        this.pt.m3 = 0;
        this.pt.s3 = this.pt.s4;
    }

    // If Auto Preset is false, then us3 default = 0
    if (!this.pt.is3) {
        this.pt.us3 = 0;
    }

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BetTypeHeader";
    box.appendChild(header); // Header bet type name
    header.style.minWidth = this.minWidth + 'px';

    // m3
    var min = this.pt.n3;
    var max = Math.min(this.pt.m4, this.pt.s4 - this.pt.s3);
    var selected = this.pt.m3;
    var m3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    m3Select.select.id = m3Select.select.name = "m3$" + this.suffixId;
    m3Select.className = "BetTypeSelect";
    m3Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(m3Select);

    // s3
    var min = 0;
    var max = this.pt.s4 - this.pt.n3;
    var selected = this.pt.s3;
    var s3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    s3Select.select.id = s3Select.select.name = "s3$" + this.suffixId;
    s3Select.className = "BetTypeSelect";
    s3Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(s3Select);

    // is3
    var is3Check = document.createElement("INPUT");
    is3Check.type = "checkbox";
    is3Check.checked = is3Check.defaultChecked = this.pt.is3;
    is3Check.name = "is3$" + this.suffixId;
    var is3Div = document.createElement("DIV");
    is3Div.className = "BetTypeSelect";
    is3Div.style.minWidth = this.minWidth + 'px';
    is3Div.appendChild(is3Check);
    box.appendChild(is3Div);
    if (is3Check.checked) {
        s3Select.select.disabled = is3Check.checked;
    }

    // us3
    min = 0;
    max = this.pt.s4;
    selected = this.pt.us3;
    var us3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    us3Select.select.id = us3Select.select.name = "us3$" + this.suffixId;
    us3Select.select.disabled = !this.pt.is3;
    us3Select.className = "BetTypeSelect";
    us3Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(us3Select);

    // is3 handling
    var s4 = this.pt.s4;
    var n3 = this.pt.n3;
    var m4 = this.pt.m4;

    is3Check.onclick = function (e) {
        // Detect current value is out of range, reset it to max
        Fanex.PTEngine.PT_ValidateSelect(this.checked ? us3Select.select : s3Select.select);

        s3Select.select.onchange(e, true);
        us3Select.select.disabled = !is3Check.checked;
        s3Select.select.disabled = is3Check.checked;
    }

    // Event change Super Preset PT
    s3Select.select.onchange = function (e, keepCurrent) {
        // Reset m3
        var max = is3Check.checked ? m4 : Math.min(m4, Fanex.Round(s4 - parseFloat(s3Select.select.value)));
        var min = n3;
        var selected = parseFloat(m3Select.select.value);

        if (!keepCurrent || selected < min || selected > max) {
            selected = max;
        }

        Fanex.PTEngine.PT_SetSelect(m3Select.select, min, max, Fanex.PTEngine.PT_STEP, selected);
    }
    box.id = "e$" + this.suffixId;
    box.className = "PTBox";

    // Get updated data
    this.Save = function () {
        this.pt.m3 = parseFloat(m3Select.select.value);
        this.pt.is3 = is3Check.checked;

        if (this.pt.is3) {
            this.pt.us3 = parseFloat(us3Select.select.value);
            this.pt.s3 = 0;
        }
        else {
            this.pt.us3 = 0;
            this.pt.s3 = parseFloat(s3Select.select.value);
        }

        return {
            id: this.id,
            pt: this.pt
        }
    }
    return box;
};
Fanex.PTEngine.BetType.prototype.Render_4_3_2 = function (owner) { // n3
    box = document.createElement("DIV");
    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BetTypeHeader";
    box.appendChild(header); // Header bet type name
    header.style.minWidth = this.minWidth + 'px';
    // n3
    var min = 0;
    var max = Math.min(this.pt.l3, this.pt.m3);
    var selected = this.pt.n3;
    var n3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
    n3Select.select.id = n3Select.select.name = "n3$" + this.suffixId;
    n3Select.className = "BetTypeSelect";
    n3Select.style.minWidth = this.minWidth + 'px';
    box.appendChild(n3Select);
    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    // Get updated data
    this.Save = function () {
        this.pt.n3 = parseFloat(n3Select.select.value);
        this.pt.l3 = 0;

        return {
            id: this.id,
            pt: this.pt
        }
    }

    return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_2_3 = function (owner) { // n2, l2
	box = document.createElement("DIV");
	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';
	//l2
	var min = 0;
	var max = 0.9;
	var selected = this.pt.l2;
	var l2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	l2Select.select.id = l2Select.select.name = "l2$" + this.suffixId;
	l2Select.className = "BetTypeSelect";
	l2Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(l2Select);
	// n2
	var min = 0;
	var max = Math.min(this.pt.l2, this.pt.a2);
	var selected = this.pt.n2;
	var n2Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	n2Select.select.id = n2Select.select.name = "n2$" + this.suffixId;
	n2Select.className = "BetTypeSelect";
	n2Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(n2Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";
	// Get updated data
	this.Save = function () {
		this.pt.n2 = parseFloat(n2Select.select.value);
		this.pt.l2 = parseFloat(l2Select.select.value);
		return {
			id: this.id,
			pt: this.pt
		}
	}
	return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_3_3 = function (owner) { // n3, l3
	box = document.createElement("DIV");
	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';
	//l3
	var min = 0;
	var max = 0.9;
	var selected = this.pt.l3;
	var l3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	l3Select.select.id = l3Select.select.name = "l3$" + this.suffixId;
	l3Select.className = "BetTypeSelect";
	l3Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(l3Select);
	// n3
	var min = 0;
	var max = Math.min(this.pt.l3, this.pt.m3);
	var selected = this.pt.n3;
	var n3Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	n3Select.select.id = n3Select.select.name = "n3$" + this.suffixId;
	n3Select.className = "BetTypeSelect";
	n3Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(n3Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";
	// Get updated data
	this.Save = function () {
		this.pt.n3 = parseFloat(n3Select.select.value);
		this.pt.l3 = parseFloat(l3Select.select.value);
		return {
			id: this.id,
			pt: this.pt
		}
	}
	return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_4_1 = function (owner) { // s4
	box = document.createElement("DIV");
	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';
	// s4
	var min = this.pt.n4;
	var max = this.pt.l4;
	var selected = this.pt.s4;
	var s4Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	s4Select.select.id = s4Select.select.name = "s4$" + this.suffixId;
	s4Select.className = "BetTypeSelect";
	s4Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(s4Select);
	s4Select.select.onchange = function () {
		var m4 = this.value;
		Fanex.PTEngine.PT_SetSelect(m4Select.select, min, m4, Fanex.PTEngine.PT_STEP, m4);
		Fanex.PTEngine.PT_SetSelect(a4Select.select, min, m4, Fanex.PTEngine.PT_STEP, m4);
	};
	// m4
	var min = 0;
	var max = this.pt.s4;
	var selected = this.pt.m4;
	var m4Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	m4Select.select.id = m4Select.select.name = "m4$" + this.suffixId;
	m4Select.className = "BetTypeSelect";
	m4Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(m4Select);
	//event
	m4Select.select.onchange = function () {
		var a4 = this.value;
		Fanex.PTEngine.PT_SetSelect(a4Select.select, min, a4, Fanex.PTEngine.PT_STEP, a4);
	};
	// a4
	var min = 0;
	var max = this.pt.m4;
	var selected = this.pt.a4;
	var a4Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	a4Select.select.id = a4Select.select.name = "a4$" + this.suffixId;
	a4Select.className = "BetTypeSelect";
	a4Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(a4Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";
	// Get updated data
	this.Save = function () {
		this.pt.s4 = parseFloat(s4Select.select.value);
		this.pt.m4 = parseFloat(m4Select.select.value);
		this.pt.a4 = parseFloat(a4Select.select.value);
		return {
			id: this.id,
			pt: this.pt
		}
	}
	return box;
};
Fanex.PTEngine.BetType.prototype.Render_0_4_2 = function (owner) { // n4, l4
	box = document.createElement("DIV");
	// Bet type header
	var header = document.createElement("DIV");
	header.innerHTML = this.name;
	header.title = this.title;
	header.className = "BetTypeHeader";
	box.appendChild(header); // Header bet type name
	header.style.minWidth = this.minWidth + 'px';
	// n4
	var min = 0;
	var max = this.pt.s4;
	var selected = this.pt.n4;
	var n4Select = Fanex.PTEngine.PT_RenderSelect(min, max, Fanex.PTEngine.PT_STEP, selected, this.showButtons);
	n4Select.select.id = n4Select.select.name = "n4$" + this.suffixId;
	n4Select.className = "BetTypeSelect";
	n4Select.style.minWidth = this.minWidth + 'px';
	box.appendChild(n4Select);
	box.id = "e$" + this.suffixId;
	box.className = "PTBox";
	// Get updated data
	this.Save = function () {
	    this.pt.n4 = parseFloat(n4Select.select.value);
	    this.pt.l4 = 0.9;
		return {
			id: this.id,
			pt: this.pt
		}
	}
	return box;
};