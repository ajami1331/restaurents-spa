import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedRestaurentListComponent } from './shared-restaurent-list.component';

describe('SharedRestaurentListComponent', () => {
  let component: SharedRestaurentListComponent;
  let fixture: ComponentFixture<SharedRestaurentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedRestaurentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedRestaurentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
