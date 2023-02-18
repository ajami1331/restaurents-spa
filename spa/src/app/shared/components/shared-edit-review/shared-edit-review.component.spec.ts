import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedEditReviewComponent } from './shared-edit-review.component';

describe('SharedEditReviewComponent', () => {
  let component: SharedEditReviewComponent;
  let fixture: ComponentFixture<SharedEditReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedEditReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedEditReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
