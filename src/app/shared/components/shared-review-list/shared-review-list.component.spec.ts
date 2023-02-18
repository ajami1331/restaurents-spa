import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedReviewListComponent } from './shared-review-list.component';

describe('SharedReviewListComponent', () => {
  let component: SharedReviewListComponent;
  let fixture: ComponentFixture<SharedReviewListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SharedReviewListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedReviewListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
