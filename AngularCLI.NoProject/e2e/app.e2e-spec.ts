import { AngularCLI.NoProjectPage } from './app.po';

describe('angular-cli.no-project App', () => {
  let page: AngularCLI.NoProjectPage;

  beforeEach(() => {
    page = new AngularCLI.NoProjectPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('cliNp works!');
  });
});
