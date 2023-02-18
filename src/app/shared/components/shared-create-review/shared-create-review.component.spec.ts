import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedCreateReviewComponent } from './shared-create-review.component';

describe('SharedCreateReviewComponent', () => {
  let component: SharedCreateReviewComponent;
  let fixture: ComponentFixture<SharedCreateReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedCreateReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedCreateReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
