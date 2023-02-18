import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedReviewReplyComponent } from './shared-review-reply.component';

describe('SharedReviewReplyComponent', () => {
  let component: SharedReviewReplyComponent;
  let fixture: ComponentFixture<SharedReviewReplyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedReviewReplyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedReviewReplyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
