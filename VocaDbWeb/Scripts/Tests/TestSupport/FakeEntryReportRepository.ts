/// <reference path="../../Shared/UrlMapper.ts" />
/// <reference path="../../Repositories/EntryReportRepository.ts" />

import EntryReportRepository from '../../Repositories/EntryReportRepository';
import UrlMapper from '../../Shared/UrlMapper';

//module vdb.tests.testSupport {

    export default class FakeEntryReportRepository extends EntryReportRepository {

        public entryReportCount: number;

        constructor() {

            super(new UrlMapper(""));

            this.getNewReportCount = (callback) => {
                if (callback)
                    callback(this.entryReportCount);
            };

        }

    }

//}