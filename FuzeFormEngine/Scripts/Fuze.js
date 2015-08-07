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
var fuze;
(function (fuze) {
    var Validator = (function (_super) {
        __extends(Validator, _super);
        function Validator() {
            _super.apply(this, arguments);
        }
        Validator.prototype.validate = function () {
            var pattern = new RegExp(this.regex);
            console.log(this.regex);
            console.log(this.value);
            return pattern.test(this.value);
            this.toggleAttribute("data-invalid", true);
        };
        __decorate([
            property()
        ], Validator.prototype, "isValid");
        __decorate([
            property()
        ], Validator.prototype, "value");
        __decorate([
            property()
        ], Validator.prototype, "regex");
        return Validator;
    })(polymer.Base);
    fuze.Validator = Validator;
})(fuze || (fuze = {}));
//# sourceMappingURL=Fuze.js.map