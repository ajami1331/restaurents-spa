import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedModalYesNoComponent } from './shared-modal-yes-no.component';

describe('SharedModalYesNoComponent', () => {
  let component: SharedModalYesNoComponent;
  let fixture: ComponentFixture<SharedModalYesNoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedModalYesNoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedModalYesNoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
