if (typeof (Fanex) == 'undefined') { // Require Fanex namespace
	Fanex = {};
}
if (typeof (Fanex.PTEngine) == 'undefined') { // Require Fanex.PTEngine namespace
	alert('Require Fanex.PTEngine namespace');
}
// Function name format Render_[RoleId_]TargetRoleId_Form
// [RoleId_] is optional
// Call method by Method.call(this, arg1, arg2...)
Fanex.PTEngine.BetTypeGroup.prototype.Render_2_1_1 = function (owner) { // a1
	// There is no decorating for this implementation
	this.rowSpan = 3; // Total cells in height
	return null;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_3_1_1 = function (owner) { // m1, um1, im1
    var box = document.createElement("DIV");
    var emptyLabel = document.createElement("DIV");
    emptyLabel.innerHTML = "&nbsp;";
    emptyLabel.className = "GroupLabel";
    box.appendChild(emptyLabel);
    var a1Label = document.createElement("DIV");
    a1Label.innerHTML = Language.AGENT;
    a1Label.title = Language.AGENT;
    a1Label.className = "GroupLabel";
    box.appendChild(a1Label);
    var m1Label = document.createElement("DIV");
    m1Label.innerHTML = Language.MASTER;
    m1Label.title = Language.MASTER;
    m1Label.className = "GroupLabel";
    box.appendChild(m1Label);
    //Allow auto PT master on member
    if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
        var um1Label = document.createElement("DIV");
        um1Label.innerHTML = Language.MASTER_AUTO;
        um1Label.title = Language.MASTER_AUTO;
        um1Label.className = "GroupLabel2";
        box.appendChild(um1Label);
        this.rowSpan = 6; // Total cells in height
    }
    else {
        this.rowSpan = 4;
    }

    box.className = "PTBox";

    return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_4_1_1 = function (owner) { // s1, us1, is1
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var a1Label = document.createElement("DIV");
	a1Label.innerHTML = Language.AGENT;
	a1Label.title = Language.AGENT;
	a1Label.className = "GroupLabel";
	box.appendChild(a1Label);
	var m1Label = document.createElement("DIV");
	m1Label.innerHTML = Language.MASTER;
	m1Label.title = Language.MASTER;
	m1Label.className = "GroupLabel";
	box.appendChild(m1Label);
	var s1Label = document.createElement("DIV");
	s1Label.innerHTML = Language.SUPER;
	s1Label.title = Language.SUPER;
	s1Label.className = "GroupLabel";
	box.appendChild(s1Label);
	var us1Label = document.createElement("DIV");
	us1Label.innerHTML = Language.SUPER_AUTO;
	us1Label.title = Language.SUPER_AUTO;
	us1Label.className = "GroupLabel2";
	box.appendChild(us1Label);
	box.className = "PTBox";
	this.rowSpan = 7; // Total cells in height
	return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_2_1 = function (owner) { // m2, a2
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var a2Label = document.createElement("DIV");
	a2Label.innerHTML = Language.AGENT_POSITION;
	a2Label.title = Language.AGENT_POSITION;
	a2Label.className = "GroupLabel";
	box.appendChild(a2Label);
	var m2Label = document.createElement("DIV");
	m2Label.innerHTML = Language.MASTER_PRESET;
	m2Label.title = Language.MASTER_PRESET_TITLE;
	m2Label.className = "GroupLabel";
	box.appendChild(m2Label);
	//Allow auto PT master on member
	if (Fanex.PTEngine.CURRENT_CONTEXT.ia3) {
		var um2Label = document.createElement("DIV");
		um2Label.innerHTML = Language.AUTO_MASTER_PRESET;
		um2Label.title = Language.AUTO_MASTER_PRESET_TITLE;
		um2Label.className = "GroupLabel2";
		box.appendChild(um2Label);
		this.rowSpan = 6; // Total cells in height
	} else {
		this.rowSpan = 4; // Total cells in height
	}
	box.className = "PTBox";

	return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_2_2 = function (owner) { // n2
	this.rowSpan = 3; // Total cells in height
	this.minWidth = 0;
	return null;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_3_1 = function (owner) { // s3, m3
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var m3Label = document.createElement("DIV");
	m3Label.innerHTML = Language.MASTER_PRESET;
	m3Label.title = Language.MASTER_PRESET_TITLE;
	m3Label.className = "GroupLabel";
	box.appendChild(m3Label);
	var s3Label = document.createElement("DIV");
	s3Label.innerHTML = Language.SUPER_PRESET;
	s3Label.title = Language.SUPER_PRESET_TITLE;
	s3Label.className = "GroupLabel";
	box.appendChild(s3Label);
	var us3Label = document.createElement("DIV");
	us3Label.innerHTML = Language.AUTO_SUPER_PRESET;
	us3Label.title = Language.AUTO_SUPER_PRESET_TITLE;
	us3Label.className = "GroupLabel2";
	box.appendChild(us3Label);
	box.className = "PTBox";

	this.rowSpan = 6; // Total cells in height
	return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_3_2 = function (owner) { // n3
	this.rowSpan = 3; // Total cells in height
	return null;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_2_3 = function (owner) { // n2, l2
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var l2Label = document.createElement("DIV");
	l2Label.innerHTML = Language.MAX_POSITION;
	l2Label.className = "GroupLabel";
	box.appendChild(l2Label);
	var n2Label = document.createElement("DIV");
	n2Label.innerHTML = Language.MIN_POSITION;
	n2Label.className = "GroupLabel";
	box.appendChild(n2Label);
	box.className = "PTBox";
	this.rowSpan = 4; // Total cells in height
	return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_3_3 = function (owner) { // n3, l3
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var l3Label = document.createElement("DIV");
	l3Label.innerHTML = Language.MAX_POSITION;
	l3Label.className = "GroupLabel";
	box.appendChild(l3Label);
	var n3Label = document.createElement("DIV");
	n3Label.innerHTML = Language.MIN_POSITION;
	n3Label.className = "GroupLabel";
	box.appendChild(n3Label);
	box.className = "PTBox";
	this.rowSpan = 4; // Total cells in height
	return box;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_4_2 = function (owner) { // n4, l4
	this.rowSpan = 3; // Total cells in height
	return null;
};
Fanex.PTEngine.BetTypeGroup.prototype.Render_0_4_1 = function (owner) { // s4
	var box = document.createElement("DIV");
	var emptyLabel = document.createElement("DIV");
	emptyLabel.innerHTML = "&nbsp;";
	emptyLabel.className = "GroupLabel";
	box.appendChild(emptyLabel);
	var s4Label = document.createElement("DIV");
	s4Label.innerHTML = Language.SUPER_MAX_POSITION;
	s4Label.className = "GroupLabel";
	box.appendChild(s4Label);
	var m4Label = document.createElement("DIV");
	m4Label.innerHTML = Language.MASTER_MAX_POSITION;
	m4Label.className = "GroupLabel";
	box.appendChild(m4Label);
	var a4Label = document.createElement("DIV");
	a4Label.innerHTML = Language.AGENT_MAX_POSITION;
	a4Label.className = "GroupLabel";
	box.appendChild(a4Label);
	box.className = "PTBox";
	this.rowSpan = 5; // Total cells in height
	return box;
};