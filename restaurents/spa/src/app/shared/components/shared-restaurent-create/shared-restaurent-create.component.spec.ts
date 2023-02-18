import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedRestaurentCreateComponent } from './shared-restaurent-create.component';

describe('SharedRestaurentCreateComponent', () => {
  let component: SharedRestaurentCreateComponent;
  let fixture: ComponentFixture<SharedRestaurentCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedRestaurentCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedRestaurentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
