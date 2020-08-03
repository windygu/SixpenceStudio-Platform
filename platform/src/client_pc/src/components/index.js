import register from './register';
import spHeader from './spHeader';
import spButtonList from './spButtonList';
import spIcon from './spIcon';
import spTable from './spTable';
import spList from './spList';
import spSection from './spSection';
import spMenu from './spMenu';
import spTag from './spTag';

const components = [
  { name: register.name, component: register },
  { name: spHeader.name, component: spHeader },
  { name: spButtonList.name, component: spButtonList },
  { name: spIcon.name, component: spIcon },
  { name: spTable.name, component: spTable },
  { name: spList.name, component: spList },
  { name: spSection.name, component: spSection },
  { name: spMenu.name, component: spMenu },
  { name: spTag.name, component: spTag }
];

export default components;
