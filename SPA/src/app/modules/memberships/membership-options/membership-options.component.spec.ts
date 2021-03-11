import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipOptionsComponent } from './membership-options.component';

describe('MembershipOptionsComponent', () => {
  let component: MembershipOptionsComponent;
  let fixture: ComponentFixture<MembershipOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MembershipOptionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MembershipOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
