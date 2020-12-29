"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_redux_1 = require("react-redux");
var Player_1 = require("../components/Player");
var home = function () {
    return (React.createElement("div", { className: "container" },
        React.createElement("div", { className: "row" },
            React.createElement("div", { className: "col-sm-12" },
                React.createElement("h1", null, "iToons Radio"),
                React.createElement("p", null, "Celebrating Hip Hop's Underground  Independent Golden Age circa 2000-2010"),
                React.createElement("p", null, "We take one album at at time, one song at a time."),
                React.createElement("hr", null),
                React.createElement("div", { className: "col-sm-12 player-bg" },
                    React.createElement(Player_1.default, null))))));
};
exports.default = react_redux_1.connect()(home);
//# sourceMappingURL=Home.js.map