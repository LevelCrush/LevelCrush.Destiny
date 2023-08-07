import Head from 'next/head';
import Image from 'next/image';
import Container from '@website/components/elements/container';
import { H2 } from '@website/components/elements/headings';
import Hero from '@website/components/hero';
import OffCanvas from '@website/components/offcanvas';
import SiteHeader from '@website/components/site_header';

export const Error404 = () => (
  <OffCanvas>
    <Head>
      <title>404 | Level Crush</title>
    </Head>
    <SiteHeader />
    <main>
      <Hero className="min-h-[20rem]">
        <Container minimalCSS={true} className="px-4 mx-auto flex-initial">
          <H2 className="drop-shadow text-center">This page does not exist</H2>
        </Container>
      </Hero>
      <Container className="flex items-center justify-center relative top-0">
        <div className="flex-initial w-[40rem] max-w-full">
          <Image
            src="https://http.cat/404"
            width="640"
            height="480"
            alt="404 Not Found"
          ></Image>
        </div>
      </Container>
    </main>
  </OffCanvas>
);

export default Error404;
