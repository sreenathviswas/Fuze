var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
if (typeof __decorate !== "function") __decorate = function (decorators, target, key, desc) {
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") return Reflect.decorate(decorators, target, key, desc);
    switch (arguments.length) {
        case 2: return decorators.reduceRight(function(o, d) { return (d && d(o)) || o; }, target);
        case 3: return decorators.reduceRight(function(o, d) { return (d && d(target, key)), void 0; }, void 0);
        case 4: return decorators.reduceRight(function(o, d) { return (d && d(target, key, o)) || o; }, desc);
    }
};
var FzTextbox = (function (_super) {
    __extends(FzTextbox, _super);
    function FzTextbox() {
        _super.apply(this, arguments);
    }
    __decorate([
        property()
    ], FzTextbox.prototype, "placeholdervalue");
    __decorate([
        property()
    ], FzTextbox.prototype, "classname");
    FzTextbox = __decorate([
        component("fz-textbox")
    ], FzTextbox);
    return FzTextbox;
})(fuze.Validator);
//# sourceMappingURL=fz-textbox.js.map