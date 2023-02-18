import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedEditRestaurantComponent } from './shared-edit-restaurant.component';

describe('SharedEditRestaurantComponent', () => {
  let component: SharedEditRestaurantComponent;
  let fixture: ComponentFixture<SharedEditRestaurantComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedEditRestaurantComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedEditRestaurantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
