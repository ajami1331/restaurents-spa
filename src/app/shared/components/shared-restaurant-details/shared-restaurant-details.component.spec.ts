import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedRestaurantDetailsComponent } from './shared-restaurant-details.component';

describe('SharedRestaurantDetailsComponent', () => {
  let component: SharedRestaurantDetailsComponent;
  let fixture: ComponentFixture<SharedRestaurantDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedRestaurantDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedRestaurantDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
