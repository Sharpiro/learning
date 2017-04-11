import { AngularCliNoProjectPage } from './app.po';

describe('angular-cli-no-project App', () => {
  let page: AngularCliNoProjectPage;

  beforeEach(() => {
    page = new AngularCliNoProjectPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
