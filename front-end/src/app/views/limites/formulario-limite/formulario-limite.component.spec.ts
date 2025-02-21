import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormularioLimiteComponent } from './formulario-limite.component';

describe('FormularioLimiteComponent', () => {
  let component: FormularioLimiteComponent;
  let fixture: ComponentFixture<FormularioLimiteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormularioLimiteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormularioLimiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
