"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var loader_component_1 = require("./loader.component");
describe('LoaderComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [loader_component_1.LoaderComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(loader_component_1.LoaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should not be visible by default', function () {
        // Arrange
        var element = fixture.nativeElement;
        var div = element.querySelectorAll('div')[0];
        // Assert
        expect(div.getAttribute('hidden')).not.toBeNull();
    });
    it('should be visible when app is loading', function () {
        // Arrange
        var element = fixture.nativeElement;
        var div = element.querySelectorAll('div')[0];
        // Act
        fixture.componentInstance.isLoading = true;
        fixture.detectChanges();
        // Assert
        expect(div.getAttribute('hidden')).toBeNull();
    });
    it('should not display a message by default', function () {
        // Arrange
        var element = fixture.nativeElement;
        var span = element.querySelectorAll('span')[0];
        // Assert
        expect(span.innerText).toBe('');
    });
    it('should display specified message', function () {
        // Arrange
        var element = fixture.nativeElement;
        var span = element.querySelectorAll('span')[0];
        // Act
        fixture.componentInstance.message = 'testing';
        fixture.detectChanges();
        // Assert
        expect(span.innerText).toBe('testing');
    });
});
//# sourceMappingURL=loader.component.spec.js.map