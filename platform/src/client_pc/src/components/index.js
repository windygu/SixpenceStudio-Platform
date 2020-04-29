import register from './register';
import spHeader from './spHeader';
import spButtonList from './spButtonList';
import spIcon from './spIcon';
import spTable from './spTable';
import spList from './spList';

const components = [
  { name: register.name, component: register },
  { name: spHeader.name, component: spHeader },
  { name: spButtonList.name, component: spButtonList },
  { name: spIcon.name, component: spIcon },
  { name: spTable.name, component: spTable },
  { name: spList.name, component: spList }
];

export default components;
