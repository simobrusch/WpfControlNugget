using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.Repository
{
    public interface IRepositoryBase<TM>
    {
        /// <summary>
        /// Liefert ein einzelnes Model-Objekt vom Typ TP zurück,
        /// welches anhand dem übergebenen PrimaryKey geladen wird.
        /// </summary>
        /// <typeparam name="TP">Type des PrimaryKey</typeparam>
        /// <param name="pkValue">Wert des PrimaryKey</param>
        /// <returns>gefundenes Model-Objekt, ansonsten null</returns>
        TM GetSingle<TP>(TP pkValue);

        /// <summary>
        /// Fügt das Model-Objekt zur Datenbank hinzu (Insert)
        /// </summary>
        /// <param name="entity">zu speicherndes Model-Object</param>
        void Add(TM entity);

        /// <summary>
        /// Löscht das Model-Objekt aus der Datenbank (Delete)
        /// </summary>
        /// <param name="entity">zu löschendes Model-Object</param>
        void Delete(TM entity);

        /// <summary>
        /// Aktualisiert das Model-Objekt in der Datenbank hinzu (Update)
        /// </summary>
        /// <param name="entity">zu aktualisierendes Model-Object</param>
        void Update(TM entity);

        /// <summary>
        /// Gibt eine Liste von Model-Objekten vom Typ TM zurück,
        /// die gemäss der WhereBedingung geladen wurden. Die Werte der
        /// Where-Bedingung können als separat übergeben werden,
        /// damit diese für PreparedStatements verwendet werden können.
        /// (Verhinderung von SQL-Injection)
        /// </summary>
        /// <param name="whereCondition">WhereBedingung als string
        /// z.B. "NetPrice > @netPrice and Active = @active and Description like @desc</param>
        /// <returns></returns>
        IQueryable<TM> GetAll(Expression<Func<TM, bool>> whereCondition);

        /// <summary>
        /// Gibt eine Liste aller in der DB vorhandenen Model-Objekte vom Typ TM zurück
        /// </summary>
        /// <returns></returns>
        IQueryable<TM> GetAll();

        IQueryable<TM> Query(string whereCondition, Dictionary<string, object> parameterValues);

        /// <summary>
        /// Zählt in der Datenbank die Anzahl Model-Objekte vom Typ TM, die der
        /// Where-Bedingung entsprechen
        /// </summary>
        /// <param name="whereCondition">WhereBedingung als string
        /// z.B. "NetPrice > @netPrice and Active = @active and Description like @desc</param>
        /// <param name="parameterValues">Parameter-Werte für die Wherebedingung
        /// bspw: {{"netPrice", 10.5}, {"active", true}, {"desc", "Wolle%"}}</param>
        /// <returns></returns>
        long Count(Expression<Func<TM, bool>> whereCondition);

        /// <summary>
        /// Zählt alle Model-Objekte vom Typ M
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
