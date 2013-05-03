// Test PT: M99RM02001
var pt = {
    s4: 0.85,
    s3: 0,
    s1: 0.3,
    m4: 0.8,
    m3: 0.8,
    m2: 0,
    m1: 0,
    a4: 0.8,
    a2: 0.6,
    a1: 0.03,
    us3: 0,
    us1: 0,
    is3: false,
    is1: false,
    um2: 0,
    um1: 0,
    im2: false,
    im1: false,
    n4: 0,
    n3: 0,
    n2: 0.03,
    l4: 0.85,
    l3: 0.8,
    l2: 0.79
};
var bettingLimits2 = {
    "bettingLimitItems": [{
        "id": 1,
        "name": "Soccer",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 2,
        "name": "Basketball",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 3,
        "name": "Football",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 5,
        "name": "Tennis",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 8,
        "name": "Baseball",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 10,
        "name": "Golf",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 11,
        "name": "Moto Sports",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 99,
        "name": "Other Sports",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 99,
        "name": "Mix Sports Parlay",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000,
        "minBetMax": 11.0000,
        "maxBetMin": 700.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 10000.0000
    }, {
        "id": 161,
        "name": "Number Game",
        "minBet": 10.0000,
        "maxBet": 100.0000,
        "maxPerMatch": 1000.0000,
        "maxPerBall": 1000.0000,
        "minBetMax": 10.0000,
        "maxBetMin": 100.0000,
        "maxPerMatchMin": 0.0000,
        "maxPerMatchMax": 1000.0000,
        "maxPerBallMax": 1000.0000
    }]
};
var bettingLimits = {
    "bettingLimitItems": [{
        "id": 1,
        "name": "Soccer",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 2,
        "name": "Basketball",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 3,
        "name": "Football",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 5,
        "name": "Tennis",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 8,
        "name": "Baseball",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 10,
        "name": "Golf",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 11,
        "name": "Moto Sports",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 99,
        "name": "Other Sports",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 99,
        "name": "Mix Sports Parlay",
        "minBet": 11.0000,
        "maxBet": 700.0000,
        "maxPerMatch": 10000.0000
    }, {
        "id": 161,
        "name": "Number Game",
        "minBet": 10.0000,
        "maxBet": 100.0000,
        "maxPerMatch": 1000.0000,
        "maxPerBall": 1000.0000
    }]
};
var grid = {
    name: 'ptGrid1',
    sportTypes: [{
        id: 1,
        name: 'Soccer',
        collapsed: false,
        betTypeGroupSets: [{
            isLive: false,
            showGroupSetLabel: true,
            betTypeGroups: [{
                id: 1,
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 7,
                    name: '1st Hdp',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 8,
                    name: '1st OU',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    minWidth: 90,
                    pt: pt
                }]
            }, {
                id: 2,
                betTypes: [{
                    id: 5,
                    name: '1X2',
                    pt: pt
                }, {
                    id: 4,
                    name: 'Correct Score',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 6,
                    name: 'Total Goal',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 9,
                    name: 'Mix Parlay',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 10,
                    name: 'Outright',
                    minWidth: 90,
                    pt: pt
                }]
            }]
        }, {
            isLive: true,
            showGroupSetLabel: true,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    minWidth: 122,
                    pt: pt
                }, {
                    id: 7,
                    name: '1st Hdp',
                    minWidth: 122,
                    pt: pt
                }, {
                    id: 8,
                    name: '1st OU',
                    minWidth: 121,
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 2,
        name: 'Basketball',
        collapsed: false,
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 3,
        name: 'Football',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 5,
        name: 'Tennis',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 8,
        name: 'Baseball',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 10,
        name: 'Golf',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 11,
        name: 'Moto Sports',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 99,
        name: 'Other Sports',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 1,
                    name: 'Handicap',
                    pt: pt
                }, {
                    id: 3,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 2,
                    name: 'Odd/Even',
                    pt: pt
                }, {
                    id: 20,
                    name: 'Money Line',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 99,
        name: 'Mix Sports Parlay',
        betTypeGroupSets: [{
            isLive: false,
            betTypeGroups: [{
                betTypes: [{
                    id: 9,
                    name: 'Mix Parlay',
                    pt: pt
                }]
            }]
        }]
    }, {
        id: 161,
        name: 'Number Game',
        betTypeGroupSets: [{
            isLive: false,
            showGroupSetLabel: true,
            betTypeGroups: [{
                betTypes: [{
                    id: 81,
                    name: '1st/Last O/U',
                    pt: pt
                }, {
                    id: 83,
                    name: '1st/Last O/E',
                    minWidth: 122,
                    pt: pt
                }, {
                    id: 88,
                    name: 'Warrior',
                    minWidth: 121,
                    pt: pt
                }, {
                    id: 86,
                    name: 'Odd/Even',
                    minWidth: 122,
                    pt: pt
                }]
            }]
        }, {
            isLive: true,
            showGroupSetLabel: true,
            betTypeGroups: [{
                betTypes: [{
                    id: 85,
                    name: 'Over/Under',
                    pt: pt
                }, {
                    id: 86,
                    name: 'Odd/Even',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 87,
                    name: 'High/Low',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 89,
                    name: 'Next Combo',
                    minWidth: 90,
                    pt: pt
                }, {
                    id: 90,
                    name: 'Number Group',
                    minWidth: 90,
                    pt: pt
                }]
            }]
        }]
    }]
}