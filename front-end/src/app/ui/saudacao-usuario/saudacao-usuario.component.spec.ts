import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaudacaoUsuarioComponent } from './saudacao-usuario.component';

describe('SaudacaoUsuarioComponent', () => {
  let component: SaudacaoUsuarioComponent;
  let fixture: ComponentFixture<SaudacaoUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaudacaoUsuarioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaudacaoUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
