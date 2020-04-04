import register from './register';
import spBlogTable from './spBlogTable';
import spHeader from './spHeader';
import spButtonList from './spButtonList';
import spIcon from './spIcon';

const components = [
  { name: register.name, component: register },
  { name: spBlogTable.name, component: spBlogTable },
  { name: spHeader.name, component: spHeader },
  { name: spButtonList.name, component: spButtonList },
  { name: spIcon.name, component: spIcon }
];

export default components;
