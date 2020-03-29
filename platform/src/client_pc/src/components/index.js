import register from './register';
import spBlogTable from './spBlogTable';
import spHeader from './spHeader';
import spButtonList from './spButtonList';

const components = [
  { name: register.name, component: register },
  { name: spBlogTable.name, component: spBlogTable },
  { name: spHeader.name, component: spHeader },
  { name: spButtonList.name, component: spButtonList }
];

export default components;
