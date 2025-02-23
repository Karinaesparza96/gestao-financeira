import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheLimiteComponent } from './detalhe-limite.component';

describe('DetalheLimiteComponent', () => {
  let component: DetalheLimiteComponent;
  let fixture: ComponentFixture<DetalheLimiteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalheLimiteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalheLimiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
