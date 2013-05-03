/// <reference path="Languages/en.js" />
if (typeof (Fanex) == 'undefined') { // Require Fanex namespace
	Fanex = {};
}
if (typeof (Fanex.PTEngine) == 'undefined') { // Require Fanex.PTEngine namespace
	alert('Require Fanex.PTEngine namespace');
}
if (typeof (Validators) == 'undefined') { // Require Validators library
	alert('Require validators library');
}
Fanex.PTEngine.BettingLimit = function (source) {
	if (typeof (source) == 'object') {
		this.items = source.bettingLimitItems;
	}
	var validateExpr = /^(\$|)([0-9]\d{0,2}(\,\d{3})*|([0-9]\d*))(\.?\d*)?$/;
	this.ValidateMessage = function (elementInput, errorMessage, xpos, pointerx) {
		var isXPos = (typeof (xpos) != "undefined" && xpos != null);
		var isPointerXPos = (typeof (pointerx) != "undefined" && pointerx != null);
		if (isXPos && pointerx) {
			Validators.show(elementInput, errorMessage, xpos, pointerx);
		}
		else {
			Validators.show(elementInput, errorMessage);
		}
		Validators.animate(elementInput);
	};
	this.Validate = function () {
		var c = this.items.length;
		for (var i = 0; i < c; i++) {
			var item = this.items[i];
			var copiedItem = item.copiedItem;
			var elementInput = null;
			// Validate each sport type
			if (copiedItem == null) { // Copy checkbox was ticked
				if (validateExpr.test(item.minBetElement.value) == false) {
					this.ValidateMessage(item.minBetElement, Language.INCORRECT_MIN_BET);
					return false;
				}
				if (validateExpr.test(item.maxBetElement.value) == false) {
					this.ValidateMessage(item.maxBetElement, Language.INCORRECT_MAX_BET);
					return false;
				}
				if (validateExpr.test(item.maxPerMatchElement.value) == false) {
					this.ValidateMessage(item.maxPerMatchElement, Language.INCORRECT_MAX_PER_MATCH);
					return false;
				}
				// Check min bet < max bet < max per match
				var minBet = parseFloat(item.minBetElement.value, true);
				var maxBet = parseFloat(item.maxBetElement.value, true);
				var maxPerMatch = parseFloat(item.maxPerMatchElement.value, true);
				if (minBet > maxBet) {
					this.ValidateMessage(item.minBetElement, Language.MIN_BET_LOWER_THAN_MIN_MAX_BET.Format(item.name));
					return false;
				}
				if (minBet > maxPerMatch) {
					this.ValidateMessage(item.minBetElement, Language.MIN_BET_LOWER_THAN_MIN_MAX_PER_MATCH_BET.Format(item.name));
					return false;
				}
				if (maxBet > maxPerMatch) {
					this.ValidateMessage(item.maxBetElement, Language.MAX_BET_LOWER_THAN_MIN_MAX_PER_MATCH_BET.Format(item.name));
					return false;
				}
				// Check minbet < minbetmax
				if (typeof (item.minBetMax) != 'undefined' && item.minBetMax > 0 && minBet < item.minBetMax && minBet != 0) {
					this.ValidateMessage(item.minBetElement, Language.MIN_BET_GREATER_THAN_MIN_MIN_BET.Format(''.FormatNumber(item.minBetMax)));
					return false;
				}
				// Check maxbet < maxbetmin
				if (typeof (item.maxBetMin) != 'undefined' && item.maxBetMin > 0 && maxBet > item.maxBetMin && maxBet != 0) {
					this.ValidateMessage(item.maxBetElement, Language.MAX_BET_LOWER_THAN_MAX_MAX_BET.Format(''.FormatNumber(item.maxBetMin)));
					return false;
				}
				// Check maxpermatch_min < maxpermatch < maxpermatch_max
				if (typeof (item.maxPerMatchMax) != 'undefined' && item.maxPerMatchMax > 0 && maxPerMatch > item.maxPerMatchMax && maxPerMatch != 0) {
					this.ValidateMessage(item.maxPerMatchElement, Language.MAX_PER_MATCH_LOWER_THAN_MAX_PER_MATCH_MAX_BET.Format(''.FormatNumber(item.maxPerMatchMax)), -130, 160);
					return false;
				}
			}
			if (item.id == 161) {
				if (validateExpr.test(item.maxPerBallElement.value) == false) {
					this.ValidateMessage(item.maxPerBallElement, Language.INCORRECT_MAX_PER_BALL);
					return false;
				}
				var maxPerBall = parseFloat(item.maxPerBallElement.value, true);
				// Check maxperball_min < maxperball < maxperball_max
				if (typeof (item.maxPerBallMax) != 'undefined' && item.maxPerBallMax > 0 && maxPerBall > item.maxPerBallMax && maxPerBall != 0) {
					this.ValidateMessage(item.maxPerBallElement, Language.MAX_PER_BALL_LOWER_THAN_MAX_PER_BALL_MAX_BET.Format(''.FormatNumber(item.maxPerBallMax)), -180, 200);
					return false;
				}
			}
		}
		// Success
		return true;
	}
	this.Save = function () {
		var bettingLimits = [];
		var c = this.items.length;
		for (var i = 0; i < c; i++) {
			var item = this.items[i];
			var bettingLimitItem = null;
			var copiedItem = item.copiedItem;
			if (copiedItem != null) { // Copy checkbox was ticked
				bettingLimitItem = {
					id: parseInt(item.id),
					key: item.key,
					MinBet: Fanex.RoundNumber(parseFloat(this.items[copiedItem.id].minBetElement.value, true), 1),
					MaxBet: Fanex.RoundNumber(parseFloat(this.items[copiedItem.id].maxBetElement.value, true), 1),
					MaxPerMatch: Fanex.RoundNumber(parseFloat(this.items[copiedItem.id].maxPerMatchElement.value, true), 1)
				};
			} else {
				bettingLimitItem = {
					id: parseInt(item.id),
					key: item.key,
					MinBet: Fanex.RoundNumber(parseFloat(item.minBetElement.value, true), 1),
					MaxBet: Fanex.RoundNumber(parseFloat(item.maxBetElement.value, true), 1),
					MaxPerMatch: Fanex.RoundNumber(parseFloat(item.maxPerMatchElement.value, true), 1)
				};
			}
			if (item.id == 161) {
				bettingLimitItem.MaxPerBall = Fanex.RoundNumber(parseFloat(item.maxPerBallElement.value, true), 1);
			}
			bettingLimits[i] = bettingLimitItem;
		}
		return {
			BettingLimitItems: bettingLimits
		};
	}

	this.Render = function (owner, hideTextBox) {
		var box = document.createElement("DIV");
		// Create MinBet, MaxBet, MaxPerMatch header
		var emptyBox = document.createElement("DIV");
		emptyBox.innerHTML = "&nbsp;";
		emptyBox.className = "BettingLimitHeader";
		box.appendChild(emptyBox);
		var minBetHeader = document.createElement("DIV");
		minBetHeader.innerHTML = Language.MIN_BET;
		minBetHeader.className = "BettingLimitHeaderTop";
		box.appendChild(minBetHeader);
		var maxBetHeader = document.createElement("DIV");
		maxBetHeader.innerHTML = Language.MAX_BET;
		maxBetHeader.className = "BettingLimitHeaderTop";
		box.appendChild(maxBetHeader);
		var maxPerMatchHeader = document.createElement("DIV");
		maxPerMatchHeader.innerHTML = Language.MAX_PER_MATCH;
		maxPerMatchHeader.className = "BettingLimitHeaderTop";
		box.appendChild(maxPerMatchHeader);
		var clear = document.createElement("DIV");
		clear.style.clear = "both";
		box.appendChild(clear);
		var showLimitBox = false;
		var c = this.items.length;
		for (var i = 0; i < c; i++) {
			var item = this.items[i];
			var box2 = document.createElement("DIV");
			if (typeof (item.minBet) == 'undefined') { // For Add new case
				item.minBet = item.minBetMax;
			}
			if (typeof (item.maxBet) == 'undefined') { // For Add new case
				item.maxBet = item.maxBetMin;
			}
			if (typeof (item.maxPerMatch) == 'undefined') { // For Add new case
				item.maxPerMatch = item.maxPerMatchMax;
			}
			if (typeof (item.maxPerBall) == 'undefined' && item.id == 161) { // For Add new case
				item.maxPerBall = item.maxPerBallMax;
			}
			var showLimitBox = typeof (item.maxBetMin) != 'undefined';
			var header = document.createElement("DIV");
			header.innerHTML = item.name;
			header.className = "BettingLimitHeader";
			item.header = header;
			box2.appendChild(header);
			// Hard-code for max per ball of number game
			if (item.id == 161) {
				// Create MinBet, MaxBet, MaxPerMatch header
				var minBetHeader161 = document.createElement("DIV");
				minBetHeader161.innerHTML = Language.MIN_BET;
				minBetHeader161.className = (showLimitBox || hideTextBox) ? "BettingLimitHeaderTop9" : "BettingLimitHeaderTop5";
				box2.appendChild(minBetHeader161);
				var maxBetHeader161 = document.createElement("DIV");
				maxBetHeader161.innerHTML = Language.MAX_BET;
				maxBetHeader161.className = (showLimitBox || hideTextBox) ? "BettingLimitHeaderTop9" : "BettingLimitHeaderTop5";
				box2.appendChild(maxBetHeader161);
				var maxPerMatchHeader161 = document.createElement("DIV");
				maxPerMatchHeader161.innerHTML = Language.MAX_PER_MATCH;
				maxPerMatchHeader161.className = (showLimitBox || hideTextBox) ? "BettingLimitHeaderTop9" : "BettingLimitHeaderTop5";
				box2.appendChild(maxPerMatchHeader161);
				var maxPerBallHeader161 = document.createElement("DIV");
				maxPerBallHeader161.innerHTML = Language.MAX_PER_BALL;
				maxPerBallHeader161.className = (showLimitBox || hideTextBox) ? "BettingLimitHeaderTop9" : "BettingLimitHeaderTop5";
				box2.appendChild(maxPerBallHeader161);
				header.className = "BettingLimitHeader2"; // 2 rows
			}
			// Current value
			// Show label instead to TextBox
			if (hideTextBox) {
				var minBetBox = document.createElement("DIV");
				minBetBox.innerHTML = ''.FormatNumber(item.minBet);
				minBetBox.className = "BettingLimitLabel";
				box2.appendChild(minBetBox);
				minBetHeader.className = "BettingLimitHeaderTop";
			}
			else {
				var minBetBox = document.createElement("DIV");
				var minBet = document.createElement("INPUT");
				minBet.type = "text";
				minBet.maxLength = 14;
				minBet.onkeyup = function (event) { Fanex.FormatNumber(event, true); };
				minBet.className = "BettingLimitTextBox";
				minBet.value = ''.FormatNumber(item.minBet);
				minBetBox.appendChild(minBet);
				minBetBox.className = "BettingLimitItem";
				box2.appendChild(minBetBox);
				item.minBetElement = minBet;
				// Limit box
				var minBetMax = document.createElement("DIV");
				if (showLimitBox) {
					minBetMax.innerHTML = item.minBetMax == 0 ? '' : '>='.FormatNumber(item.minBetMax);
					minBetMax.className = "BettingLimitItem2";
					box2.appendChild(minBetMax);
				} else {
					minBet.className = "BettingLimitTextBox4";
					minBetBox.className = "BettingLimitItem4";
					minBetHeader.className = "BettingLimitHeaderTop4";
				}
			}

			// Current value
			// Show label instead to TextBox
			if (hideTextBox) {
				var maxBetBox = document.createElement("DIV");
				maxBetBox.innerHTML = ''.FormatNumber(item.maxBet);
				maxBetBox.className = "BettingLimitLabel";
				box2.appendChild(maxBetBox);
				maxBetHeader.className = "BettingLimitHeaderTop";
			}
			else {
				var maxBetBox = document.createElement("DIV");
				var maxBet = document.createElement("INPUT");
				maxBet.type = "text";
				maxBet.maxLength = 14;
				maxBet.onkeyup = function (event) { Fanex.FormatNumber(event, true); };
				maxBet.className = "BettingLimitTextBox";
				maxBet.value = ''.FormatNumber(item.maxBet);
				maxBetBox.appendChild(maxBet);
				maxBetBox.className = "BettingLimitItem";
				box2.appendChild(maxBetBox);
				item.maxBetElement = maxBet;
				// Limit box
				var maxBetMin = document.createElement("DIV");
				if (showLimitBox) {
					maxBetMin.innerHTML = item.maxBetMin == 0 ? '' : '<='.FormatNumber(item.maxBetMin);
					maxBetMin.className = "BettingLimitItem2";
					box2.appendChild(maxBetMin);
				} else {
					maxBet.className = "BettingLimitTextBox4";
					maxBetBox.className = "BettingLimitItem4";
					maxBetHeader.className = "BettingLimitHeaderTop4";
				}
			}

			// Current value
			// Show label instead to TextBox
			if (hideTextBox) {
				var maxPerMatchBox = document.createElement("DIV");
				maxPerMatchBox.innerHTML = ''.FormatNumber(item.maxPerMatch);
				maxPerMatchBox.className = "BettingLimitLabel";
				box2.appendChild(maxPerMatchBox);
				maxPerMatchHeader.className = "BettingLimitHeaderTop";

				if (item.id == 161) {
					minBetBox.className = maxBetBox.className = maxPerMatchBox.className = "BettingLimitLabel1";

					var maxPerBallBox = document.createElement("DIV");
					maxPerBallBox.className = "BettingLimitLabel1";
					maxPerBallBox.innerHTML = ''.FormatNumber(item.maxPerBall);
					box2.appendChild(maxPerBallBox);
				}
			}
			else {
				var maxPerMatchBox = document.createElement("DIV");
				var maxPerMatch = document.createElement("INPUT");
				maxPerMatch.type = "text";
				maxPerMatch.maxLength = 14;
				maxPerMatch.onkeyup = function (event) { Fanex.FormatNumber(event, true); };
				maxPerMatch.className = "BettingLimitTextBox";
				maxPerMatch.value = ''.FormatNumber(item.maxPerMatch);
				maxPerMatchBox.appendChild(maxPerMatch);
				maxPerMatchBox.className = "BettingLimitItem";
				box2.appendChild(maxPerMatchBox);
				item.maxPerMatchElement = maxPerMatch;
				// Limit box
				var maxPerMatchMax = document.createElement("DIV");
				if (showLimitBox) {
					maxPerMatchMax.innerHTML = item.maxPerMatchMax == 0 ? '' : '<='.FormatNumber(item.maxPerMatchMax);
					maxPerMatchMax.className = "BettingLimitItem2";
					box2.appendChild(maxPerMatchMax);
				} else {
					maxPerMatch.className = "BettingLimitTextBox4";
					maxPerMatchBox.className = "BettingLimitItem4";
					maxPerMatchHeader.className = "BettingLimitHeaderTop4";
				}
				if (item.id == 161) {
					minBet.className = showLimitBox ? 'BettingLimitTextBox9' : 'BettingLimitTextBox5';
					minBetBox.className = showLimitBox ? 'BettingLimitItem9' : 'BettingLimitItem5';
					minBetMax.className = "BettingLimitItem92";
					maxBet.className = showLimitBox ? 'BettingLimitTextBox9' : 'BettingLimitTextBox5';
					maxBetBox.className = showLimitBox ? 'BettingLimitItem9' : 'BettingLimitItem5';
					maxBetMin.className = "BettingLimitItem92";
					maxPerMatch.className = showLimitBox ? 'BettingLimitTextBox9' : 'BettingLimitTextBox5';
					maxPerMatchBox.className = showLimitBox ? 'BettingLimitItem9' : 'BettingLimitItem5';
					maxPerMatchMax.className = "BettingLimitItem92";
					// Max Per Ball
					var maxPerBallBox = document.createElement("DIV");
					var maxPerBall = document.createElement("INPUT");
					maxPerBall.type = "text";
					maxPerBall.maxLength = 14;
					maxPerBall.onkeyup = function (event) { Fanex.FormatNumber(event, true); };
					maxPerBall.className = showLimitBox ? 'BettingLimitTextBox9' : 'BettingLimitTextBox5';
					maxPerBall.value = ''.FormatNumber(item.maxPerBall);
					maxPerBallBox.appendChild(maxPerBall);
					maxPerBallBox.className = showLimitBox ? 'BettingLimitItem9' : 'BettingLimitItem5';
					box2.appendChild(maxPerBallBox);
					item.maxPerBallElement = maxPerBall;
					// Limit box
					var maxPerBallMax = document.createElement("DIV");
					if (showLimitBox) {
						maxPerBallMax.innerHTML = item.maxPerBallMax == 0 ? '' : '<='.FormatNumber(item.maxPerBallMax);
						maxPerBallMax.className = "BettingLimitItem92";
						box2.appendChild(maxPerBallMax);
					}
				}
			}
			item.element = box2;
			box.appendChild(box2);
			var clear = document.createElement("DIV");
			clear.style.clear = "both";
			box.appendChild(clear);
		}
		box.className = (showLimitBox || hideTextBox) ? "BettingLimit" : "BettingLimit4";
		if (typeof (owner) == 'object') {
			if (!hideTextBox) {
				var headerSoccer = this.items[0].header;
				var headerBasketBall = this.items[1].header;
				headerSoccer.className = headerBasketBall.className = "BettingLimitHeaderCopied";
				headerSoccer.title = Language.COPY_ALL_BETTING_LIMIT_FROM.Format(Language.SOCCER);
				headerBasketBall.title = Language.COPY_ALL_BETTING_LIMIT_FROM.Format(Language.BASKETBALL);
				var items = this.items;
				items[0].active = items[1].active = false;
				// Handle event copy from soccer
				headerSoccer.onclick = function () {
					var c = items.length - 1;
					if (items[0].active === false) { // Uncheck --> Checked
						items[0].active = true;
						headerSoccer.className = "BettingLimitHeaderCopiedActive";
						if (items[1].active === true) { // Untick basket ball
							items[1].active = false;
							headerBasketBall.className = "BettingLimitHeaderCopied";
						}
						for (var i = 1; i < c; i++) {
							var item = items[i];
							item.copiedItem = {
								id: 0
							}; // Copied from Soccer, using index

							item.inactive = true;
							Fanex.PTEngine.DisableChildren(item.element, "BetTypeInactive");
						}
					} else {
						items[0].active = false;
						headerSoccer.className = "BettingLimitHeaderCopied";
						for (var i = 1; i < c; i++) {
							var item = items[i];
							item.copiedItem = null; // Remove copiedItem
							if (item.inactive === false) break;
							item.inactive = false;
							Fanex.PTEngine.ReEnableChildren(item.element);
						}
					}
				}
				// Handle event copy from basket ball
				headerBasketBall.onclick = function () {
					if (items[1].inactive == true) { // Re-enable current
						items[1].inactive = false;
						items[1].copiedItem = null; // Remove copiedItem
						Fanex.PTEngine.ReEnableChildren(items[1].element);
					}
					var c = items.length - 1;
					if (items[1].active === false) { // Uncheck --> Checked
						items[1].active = true;
						headerBasketBall.className = "BettingLimitHeaderCopiedActive";
						if (items[0].active === true) { // Untick soccer
							items[0].active = false;
							headerSoccer.className = "BettingLimitHeaderCopied";
						}
						for (var i = 2; i < c; i++) {
							var item = items[i];
							item.copiedItem = {
								id: 1
							}; // Copied from Basketball, using index

							item.inactive = true;
							Fanex.PTEngine.DisableChildren(item.element, "BetTypeInactive");
						}
					} else {
						items[1].active = false;
						headerBasketBall.className = "BettingLimitHeaderCopied";
						for (var i = 2; i < c; i++) {
							var item = items[i];
							item.copiedItem = null; // Remove copiedItem
							if (item.inactive === false) break;
							item.inactive = false;
							Fanex.PTEngine.ReEnableChildren(item.element);
						}
					}
				}
			}
			owner.appendChild(box);
		}
		return box;
	}
}